using WKExample.Shared.Exceptions;

namespace WKExample.Domain.Exceptions.RegistrationNumber
{
    public class CannotUpdateRegistrationNumberException : WKDomainException
    {
        public CannotUpdateRegistrationNumberException() : base("Registration number is already set and cannot be edited.")
        {
        }
    }
}
