using System;
using WKExample.Shared.Exceptions;

namespace WKExample.Domain.Exceptions
{
    public class EmployeeNotFoundException : WKDomainException
    {
        public EmployeeNotFoundException(Guid id) : base($"Employee with given id ({id}) does not exist.")
        {
        }
    }
}
