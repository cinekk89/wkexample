using WKExample.Shared.Exceptions;

namespace WKExample.Domain.Exceptions.RegistrationNumber
{
    public class IncorrectRegistrationNumberSecondPartException : WKDomainException
    {
        public IncorrectRegistrationNumberSecondPartException() : base("Wrong registration number's second part")
        {
        }
    }
}
