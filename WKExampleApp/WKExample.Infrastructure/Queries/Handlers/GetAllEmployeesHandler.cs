using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WKExample.Application.DTOs;
using WKExample.Application.Queries;
using WKExample.Domain.Repositories;

namespace WKExample.Infrastructure.Queries.Handlers
{
    public class GetAllEmployeesHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetAllEmployeesHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.Get();

            return employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                RegistrationNumber = e.RegistrationNumber.ToString(),
                Pesel = e.Pesel,
                LastName = e.LastName,
                FirstName = e.FirstName,
                SecondName = e.SecondName,
                DateOfBirth = e.DateOfBirth,
                Gender = e.Gender,
            });
        }
    }
}
