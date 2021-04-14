using WKExample.Shared.Exceptions;

namespace WKExample.Domain.Exceptions.Employee
{
    public class WrongEmployeeNamesException : WKDomainException
    {
        public WrongEmployeeNamesException() : base("Employee first or/and second name is too long")
        {
        }
    }
}
