using System;
using System.Collections.Generic;
using System.Linq;
using WKExample.Domain.Exceptions.RegistrationNumber;

namespace WKExample.Domain.Entities
{
    public class RegistrationNumber
    {
        protected const int SecondPartLength = 6;
        protected const char DefaultSecondPartFiller = '0';

        public string FirstPart => "1-";
        public string SecondPart { get; private set; }

        protected RegistrationNumber()
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



        public override string ToString()
        {
            return FirstPart + SecondPart;
        }

        private void SetNewNumber(IEnumerable<RegistrationNumber> unavailableRegistrationNumbers)
        {
            var currentMaxNumber = unavailableRegistrationNumbers.Select(n => Int32.Parse(n.SecondPart)).Max();
            var newSecondPartNumber = ++currentMaxNumber;

            if (!IsSecondPartNumberValid(newSecondPartNumber))
            {
                newSecondPartNumber = GetFirstAvailableNumber(unavailableRegistrationNumbers);
                if (!IsSecondPartNumberValid(newSecondPartNumber))
                {
                    throw new RegistrationNumbersReachedMaxException();
                }
            }

            SecondPart = FillSecondPart(newSecondPartNumber);
        }

        private void SetSecondPart(string registrationNumber)
        {
            if (!string.IsNullOrWhiteSpace(SecondPart))
            {
                throw new CannotUpdateRegistrationNumberException();
            }

            var secondPart = registrationNumber?.Replace(FirstPart, string.Empty);
            if (string.IsNullOrWhiteSpace(registrationNumber) || !registrationNumber.StartsWith(FirstPart) || secondPart.Length != SecondPartLength || !Int32.TryParse(secondPart, out _))
            {
                throw new IncorrectRegistrationNumberSecondPartException();
            }

            SecondPart = secondPart;
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

        private bool IsSecondPartNumberValid(int number)
        {
            var numberLength = number.ToString().Length;
            return number > 0 && numberLength <= SecondPartLength;
        }

        private static string FillSecondPart(int secondPartNumber)
        {
            return secondPartNumber.ToString().PadLeft(SecondPartLength, DefaultSecondPartFiller);
        }
    }
}
