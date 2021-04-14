using WKExample.Shared.Exceptions;

namespace WKExample.Domain.Exceptions.RegistrationNumber
{
    public class RegistrationNumbersReachedMaxException : WKDomainException
    {
        public RegistrationNumbersReachedMaxException() : base("Registration number reached maximum value.")
        {
        }
    }
}
