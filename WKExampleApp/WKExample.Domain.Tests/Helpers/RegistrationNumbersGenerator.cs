using System.Collections.Generic;
using System.Linq;
using WKExample.Domain.Entities;

namespace WKExample.Domain.Tests.Helpers
{
    static class RegistrationNumbersGenerator
    {
        const string FirstPart = "1-";
        const int SecondPartLength = 6;
        const char SecondPartFiller = '0';

        internal static HashSet<RegistrationNumber> Generate(int limit = 1000000, params int[] numbersToSkip)
        {
            var allNumbers = new HashSet<RegistrationNumber>();

            for (var i = 1; i < limit; i++)
            {
                if (numbersToSkip.Any(number => number == i))
                    continue;

                allNumbers.Add(new RegistrationNumber($"{FirstPart}{i.ToString().PadLeft(SecondPartLength, SecondPartFiller)}"));
            }

            return allNumbers;
        }
    }
}
