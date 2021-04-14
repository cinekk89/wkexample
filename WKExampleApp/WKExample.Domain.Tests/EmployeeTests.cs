using NUnit.Framework;
using System;
using WKExample.Domain.Entities;
using WKExample.Domain.Exceptions.Employee;
using WKExample.Domain.Tests.Helpers;

namespace WKExample.Domain.Tests
{
    public class EmployeeTests
    {
        const string CorrectPesel = "88032300141";
        DateTime Today;
        Employee _sut;

        [SetUp]
        public void SetUp()
        {
            Today = new DateTime(2021, 4, 14);
            _sut = new Employee(Guid.NewGuid(), new RegistrationNumber("1-000001"), CorrectPesel, new DateTime(1990, 8, 25), "TestLastName", "FirstName", "SecondName", "Male", Today);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        [TestCase("incorrect_1")]
        [TestCase("000004123112")]
        public void SetPesel_ShouldThrowIncorrectEmployeePeselException_When_Pesel_Is_Incorrect(string pesel)
        {
            Assert.Throws<IncorrectEmployeePeselException>(() => _sut.SetPesel(pesel));
        }

        [Test]
        public void SetPesel_ShouldSetPesel_When_Pesel_Is_Correct()
        {
            _sut.SetPesel(CorrectPesel);
            Assert.AreEqual(CorrectPesel, _sut.Pesel);
        }

        [TestCase("TooLongTestNameTooLongTest", "CorrectName")]
        [TestCase("CorrectSecondName", "TooLongTestNameTooLongTestName")]
        [TestCase(null, "CorrectName")]
        [TestCase("TooLongTestNameTooLongTestName", null)]
        [TestCase("", "CorrectName")]
        [TestCase(" ", "CorrectName")]
        public void SetNames_ShouldThrowWrongEmployeeNamesException_When_Names_Are_Incorrect(string firstName, string secondName)
        {
            Assert.Throws<WrongEmployeeNamesException>(() => _sut.SetNames(firstName, secondName));
        }

        [TestCase("CorrectName", "CorrectName")]
        [TestCase("CorrectName", null)]
        public void SetNames_ShouldSetName_When_Names_Are_Correct(string firstName, string secondName)
        {
            _sut.SetNames(firstName, secondName);
            Assert.AreEqual(firstName, _sut.FirstName);
            Assert.AreEqual(secondName, _sut.SecondName);
        }

        [TestCase("CorrectName", null)]
        [TestCase("CorrectName", "")]
        [TestCase("CorrectName", " ")]
        public void SetNames_ShouldSetSecondName_When_Null_If_Is_Empty_Or_WhiteSpace(string firstName, string secondName)
        {
            _sut.SetNames(firstName, secondName);
            Assert.AreEqual(null, _sut.SecondName);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        [TestCase("TooLongTestLastNameTooLongTooLongTestLastNameTooLon")]
        public void SetLastName_ShouldThrowWrongEmployeeLastNameException_When_LastName_Is_Incorrect(string lastName)
        {
            Assert.Throws<WrongEmployeeLastNameException>(() => _sut.SetLastName(lastName));

        }

        [TestCase("LastName")]
        [TestCase("CorrectLastNameCorrectLastNameCorrectLastNameLastN")]
        public void SetLastName_ShouldSetLastName_When_Names_Are_Correct(string lastName)
        {
            _sut.SetLastName(lastName);
            Assert.AreEqual(lastName, _sut.LastName);
        }

        [Test]
        public void SetDateOfBirth_ShouldThrowWrongEmployeeDateOfBirthException_When_DateOfBirth_Is_NotSet()
        {
            Assert.Throws<WrongEmployeeDateOfBirthException>(() => _sut.SetDateOfBirth(DateTime.MinValue, Today));

        }

        [Test]
        public void SetDateOfBirth_ShouldThrowWrongEmployeeDateOfBirthException_When_DateOfBirth_Is_Equal_Or_Greater_Than_Today()
        {
            Assert.Throws<WrongEmployeeDateOfBirthException>(() => _sut.SetDateOfBirth(Today, Today));
            Assert.Throws<WrongEmployeeDateOfBirthException>(() => _sut.SetDateOfBirth(Today.AddDays(2), Today));
        }

        [Test]
        public void SetDateOfBirth_ShouldSetDateOfBirth_When_DateOfBirth_Is_Correct()
        {
            var dateOfBirth = Today.AddYears(-40);
            _sut.SetDateOfBirth(dateOfBirth, Today);
            Assert.AreEqual(dateOfBirth, _sut.DateOfBirth);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("TestGender")]
        [TestCase("male")]
        public void SetGender_ShouldThrowIncorrectEmployeeGenderException_When_Gender_Is_Incorrect(string gender)
        {
            Assert.Throws<IncorrectEmployeeGenderException>(() => _sut.SetGender(gender));
        }

        [TestCase("Female")]
        [TestCase("Male")]
        public void SetGender_ShouldSetGender_When_Gender_Is_Correct(string gender)
        {
            _sut.SetGender(gender);
            Assert.AreEqual(gender, _sut.Gender);
        }

        [Test]
        public void Create_ShouldCreateEmployee_With_RegistrationNumber_And_Id_Filled()
        {
            var dateOfBirth = Today.AddYears(-40);
            const int firstAvailableNumber = 699;
            string expectedRegistrationNumber = $"1-000{firstAvailableNumber}";
            var unavailableNumbers = RegistrationNumbersGenerator.Generate(firstAvailableNumber);


            var sut = Employee.Create(CorrectPesel, dateOfBirth, "LastName", "FirstName", null, "Male", Today, unavailableNumbers);
            Assert.IsTrue(sut.Id != Guid.Empty, "Employee Id should not be empty");
            Assert.IsTrue(sut.RegistrationNumber != null, "Employee Registration Number should not be null");
            Assert.AreEqual(expectedRegistrationNumber, sut.RegistrationNumber.ToString());
        }
    }
}