using NUnit.Framework;
using WKExample.Domain.Entities;
using WKExample.Domain.Exceptions.RegistrationNumber;
using WKExample.Domain.Tests.Helpers;

namespace WKExample.Domain.Tests
{
    public class RegistrationNumberTests
    {
        const string CorrectRegistrationNumber = "1-000001";

        [Test]
        public void ShouldAssignSecondPart_When_RegistrationNumber_Is_Correct()
        {
            var sut = new RegistrationNumber(CorrectRegistrationNumber);

            Assert.AreEqual(CorrectRegistrationNumber, $"{sut.FirstPart}{sut.SecondPart}");
        }

        [Test]
        public void ToString_ShouldReturnFullRegistrationNumber_When_RegistrationNumber_Is_Correct()
        {
            var sut = new RegistrationNumber(CorrectRegistrationNumber);

            Assert.AreEqual(CorrectRegistrationNumber, sut.ToString());
        }

        [TestCase("2-124123")]
        [TestCase("1-32123")]
        [TestCase("1-1241233")]
        [TestCase("1-asda44")]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void ShouldThrowIncorrectRegistrationNumberSecondPartException_When_RegistrationNumber_Is_Incorrect(string incorrectRegistrationNumber)
        {
            Assert.Throws<IncorrectRegistrationNumberSecondPartException>(() => new RegistrationNumber(incorrectRegistrationNumber));
        }

        [Test]
        public void Create_ShouldThrowRegistrationNumbersReachedMaxException_When_RegistrationNumber_Reached_Max()
        {
            var unavailableNumbers = RegistrationNumbersGenerator.Generate();

            Assert.Throws<RegistrationNumbersReachedMaxException>(() => RegistrationNumber.Create(unavailableNumbers));
        }

        [Test]
        public void Create_ShouldCreateNextNewRegistrationNumber_When_Next_Is_Available()
        {
            int nextAvailableNumber = 434;
            string expectedRegistrationNumber = $"1-000{nextAvailableNumber}";
            var unavailableNumbers = RegistrationNumbersGenerator.Generate(limit: nextAvailableNumber);

            var sut = RegistrationNumber.Create(unavailableNumbers);

            Assert.AreEqual(expectedRegistrationNumber, sut.FirstPart + sut.SecondPart);
        }

        [Test]
        public void Create_ShouldCreateNextNewRegistrationNumber_When_Next_Is_Available_And_Are_Gapps_In_Numbers()
        {
            const int nextAvailableNumber = 99999;
            string expectedRegistrationNumber = $"1-0{nextAvailableNumber}";
            var unavailableNumbers = RegistrationNumbersGenerator.Generate(limit: nextAvailableNumber);

            var sut = RegistrationNumber.Create(unavailableNumbers);

            Assert.AreEqual(expectedRegistrationNumber, sut.FirstPart + sut.SecondPart);
        }

        [Test]
        public void Create_ShouldCreateFirstMissingAvailableRegistrationNumber_When_Are_Gapps_In_Numbers_And_RegistrationNumbers_Reached_Max()
        {
            int firstAvailableNumber = 423;
            int[] missingRegistrationNumbers = new int[] { firstAvailableNumber, 23441 };
            string expectedRegistrationNumber = $"1-000{firstAvailableNumber}";
            var unavailableNumbers = RegistrationNumbersGenerator.Generate(numbersToSkip: missingRegistrationNumbers);

            var sut = RegistrationNumber.Create(unavailableNumbers);

            Assert.AreEqual(expectedRegistrationNumber, sut.FirstPart + sut.SecondPart);
        }
    }
}
