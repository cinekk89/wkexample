using WKExample.Shared.Exceptions;

namespace WKExample.Domain.Exceptions
{
    public class IncorrectEmployeeGenderException : WKDomainException
    {
        public IncorrectEmployeeGenderException() : base($"Selected employee gender is incorrect.")
        {
        }
    }
}
