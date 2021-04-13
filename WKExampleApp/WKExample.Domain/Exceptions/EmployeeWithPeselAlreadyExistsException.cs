using WKExample.Shared.Exceptions;

namespace WKExample.Domain.Exceptions
{
    public class EmployeeWithPeselAlreadyExistsException : WKDomainException
    {
        public EmployeeWithPeselAlreadyExistsException(string pesel) : base($"Employee with given pesel ({pesel}) already exists.")
        {
        }
    }
}
