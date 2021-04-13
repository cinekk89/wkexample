using MediatR;
using System.Collections.Generic;
using WKExample.Application.DTOs;

namespace WKExample.Application.Queries
{
    public class GetAllEmployeesQuery : IRequest<IEnumerable<EmployeeDto>>
    {
    }
}
