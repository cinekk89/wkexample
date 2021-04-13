using WKExample.Shared.Exceptions;

namespace WKExample.Domain.Exceptions
{
    public class WrongEmployeeDateOfBirthException : WKDomainException
    {
        public WrongEmployeeDateOfBirthException() : base("Given date of birth is incorrect.")
        {
        }
    }
}
