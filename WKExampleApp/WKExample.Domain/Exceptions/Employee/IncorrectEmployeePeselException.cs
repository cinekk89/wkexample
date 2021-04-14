using WKExample.Shared.Exceptions;

namespace WKExample.Domain.Exceptions.Employee
{
    public class IncorrectEmployeePeselException : WKDomainException
    {
        public IncorrectEmployeePeselException(string pesel) : base($"Given PESEL is incorrect ({pesel})")
        {
        }
    }
}
