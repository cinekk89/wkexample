using System;
using System.Collections.Generic;
using System.Linq;

namespace WKExample.Domain.Entities
{
    public sealed class RegistrationNumber
    {
        private const int SecondPartLength = 6;
        private const char DefaultSecondPartFiller = '0';

        public string FirstPart => "1-";
        public string SecondPart { get; private set; }

        private RegistrationNumber()
        {
        }

        public RegistrationNumber(string registrationNumber)
        {
            SetSecondPart(registrationNumber);
        }

        public static RegistrationNumber Create(IEnumerable<RegistrationNumber> unavailableRegistrationNumbers)
        {
            var registrationNumber = new RegistrationNumber();
            registrationNumber.SetNewNumber(unavailableRegistrationNumbers);
            return registrationNumber;
        }

        public HashSet<RegistrationNumber> Generator() // todo do wywalenia
        {
            var allNumbers = new HashSet<RegistrationNumber>();

            for (var i = 1; i < 1000000; i++)
            {
                if (i == 10059)
                    continue;

                allNumbers.Add(new RegistrationNumber(i.ToString().PadLeft(SecondPartLength, DefaultSecondPartFiller)));
            }

            return allNumbers;
        }

        public void SetSecondPart(string registrationNumber)
        {
            if (!string.IsNullOrWhiteSpace(SecondPart))
            {
                throw new ArgumentException("Registration number is already set and cannot be edited."); //todo change to domain exceptions
            }

            var secondPart = registrationNumber.Replace(FirstPart, string.Empty);
            if (secondPart.Length != SecondPartLength || !Int32.TryParse(secondPart, out var number))
            {
                throw new ArgumentException("Wrong registration number's second part");
            }

            SecondPart = secondPart;
        }

        public void SetNewNumber(IEnumerable<RegistrationNumber> unavailableRegistrationNumbers)
        {
            var currentMaxNumber = unavailableRegistrationNumbers.Select(n => Int32.Parse(n.SecondPart)).Max();
            var newSecondPartNumber = ++currentMaxNumber;

            if (!IsSecondPartNumberValid(newSecondPartNumber))
            {
                newSecondPartNumber = GetFirstAvailableNumber(unavailableRegistrationNumbers);
                if (!IsSecondPartNumberValid(newSecondPartNumber))
                {
                    throw new ArgumentException("Second part of registration number reached maximum value.");
                }
            }

            SecondPart = FillSecondPart(newSecondPartNumber);
        }

        public override string ToString()
        {
            return FirstPart + SecondPart;
        }

        private bool IsSecondPartNumberValid(int number)
        {
            var numberLength = number.ToString().Length;
            return number > 0 && numberLength <= SecondPartLength;
        }

        private int GetFirstAvailableNumber(IEnumerable<RegistrationNumber> existingRegistrationNumbers)
        {
            int minNumber = 1;
            var secondPartUnavailableNumbers = existingRegistrationNumbers.Select(n => Int32.Parse(n.SecondPart)).ToHashSet();
            int maxNumber = secondPartUnavailableNumbers.Max();

            var numbersInRange = new HashSet<int>(Enumerable.Range(minNumber, maxNumber));
            numbersInRange.ExceptWith(secondPartUnavailableNumbers);

            return numbersInRange.Any() ? numbersInRange.Min() : 0;
        }

        private static string FillSecondPart(int secondPartNumber)
        {
            return secondPartNumber.ToString().PadLeft(SecondPartLength, DefaultSecondPartFiller);
        }
    }
}
