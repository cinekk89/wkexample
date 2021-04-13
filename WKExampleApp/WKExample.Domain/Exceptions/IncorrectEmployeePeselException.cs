using System;
using WKExample.Shared.Exceptions;

namespace WKExample.Domain.Exceptions
{
    public class IncorrectEmployeePeselException : WKDomainException
    {
        public IncorrectEmployeePeselException(string pesel) : base($"Given PESEL is incorrect ({pesel})")
        {
        }
    }
}
