using System;
using System.Collections.Generic;
using WKExample.Domain.Consts;
using WKExample.Domain.Exceptions.Employee;
using WKExample.Domain.Kernel;

namespace WKExample.Domain.Entities
{
    public sealed class Employee : AggregateRoot
    {
        public Guid Id { get; private set; }
        public RegistrationNumber RegistrationNumber { get; private set; }
        public string Pesel { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string SecondName { get; private set; }
        public string Gender { get; private set; }


        private Employee()
        {
        }

        public Employee(Guid id, RegistrationNumber registrationNumber, string pesel, DateTime dateOfBirth, string lastName, string firstName, string secondName,
            string gender, DateTime now, int version = 0)
        {
            Id = id;
            SetRegistrationNumber(registrationNumber);
            SetPesel(pesel);
            SetDateOfBirth(dateOfBirth, now);
            SetLastName(lastName);
            SetNames(firstName, secondName);
            SetGender(gender);
        }

        public static Employee Create(string pesel, DateTime dateOfBirth, string lastName, string firstName, string secondName,
            string gender, DateTime now, IEnumerable<RegistrationNumber> unavailableRegistrationNumbers)
        {
            var employee = new Employee();
            employee.SetId();
            employee.RegistrationNumber = RegistrationNumber.Create(unavailableRegistrationNumbers);
            employee.SetPesel(pesel);
            employee.SetDateOfBirth(dateOfBirth, now);
            employee.SetLastName(lastName);
            employee.SetNames(firstName, secondName);
            employee.SetGender(gender);

            //for the future -> add EmployeeCreated DomainEvent

            return employee;
        }

        public void SetPesel(string pesel)
        {
            /*1.creating proper validation for Pesel is bigger than the whole task itself,
              2.using third-party library (like https://github.com/asienicki/PESEL) in domain is not a good solution
              3.copy/past validation from this library and than UT it (test are also included in library) would make no sense
              for the purposes of this task
              
              That's why I leave it with this simple validation and as a primitive type instead of Value Object*/
            if (string.IsNullOrWhiteSpace(pesel) || pesel.Length != 11 || !Int64.TryParse(pesel, out _))
            {
                throw new IncorrectEmployeePeselException(pesel);
            }

            Pesel = pesel;
        }

        public void SetNames(string firstName, string secondName = null)
        {
            const int maxNameLength = 25;

            if (string.IsNullOrWhiteSpace(secondName))
                secondName = null;

            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > maxNameLength)
            {
                throw new WrongEmployeeNamesException();
            }

            if (!string.IsNullOrWhiteSpace(secondName) && secondName.Length > maxNameLength)
            {
                throw new WrongEmployeeNamesException();
            }

            FirstName = firstName;
            SecondName = secondName;
        }

        public void SetLastName(string lastName)
        {
            const int maxLastNameLength = 50;

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > maxLastNameLength)
            {
                throw new WrongEmployeeLastNameException();
            }

            LastName = lastName;
        }

        public void SetDateOfBirth(DateTime dateOfBirth, DateTime now)
        {
            if (dateOfBirth == DateTime.MinValue || dateOfBirth >= now)
            {
                throw new WrongEmployeeDateOfBirthException();
            }

            DateOfBirth = dateOfBirth;
        }

        public void SetGender(string gender)
        {
            if (!GenderEnum.IsValid(gender))
            {
                throw new IncorrectEmployeeGenderException();
            }

            Gender = gender;
        }

        private void SetId()
        {
            Id = Guid.NewGuid();
        }

        private void SetRegistrationNumber(RegistrationNumber registrationNumber)
        {
            if (registrationNumber is null)
            {
                throw new EmptyRegistrationNumberException();
            }

            RegistrationNumber = registrationNumber;
        }
    }
}
