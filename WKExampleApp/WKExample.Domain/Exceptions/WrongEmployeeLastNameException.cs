using WKExample.Shared.Exceptions;

namespace WKExample.Domain.Exceptions
{
    public class WrongEmployeeLastNameException : WKDomainException
    {
        public WrongEmployeeLastNameException() : base("Employee last name is too long.")
        {
        }
    }
}
