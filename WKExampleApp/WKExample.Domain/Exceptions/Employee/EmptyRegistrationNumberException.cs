using WKExample.Shared.Exceptions;

namespace WKExample.Domain.Exceptions.Employee
{
    public class EmptyRegistrationNumberException : WKDomainException
    {
        public EmptyRegistrationNumberException() : base("Registration number Cannot be empty.")
        {
        }
    }
}
