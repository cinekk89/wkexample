using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WKExample.Application.DTOs;
using WKExample.Application.Queries;
using WKExample.Domain.Exceptions;
using WKExample.Domain.Repositories;

namespace WKExample.Infrastructure.Queries.Handlers
{
    public class GetEmployeeHandler : IRequestHandler<GetEmployeeQuery, EmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employee = _employeeRepository.Get(request.Id);

            if (employee is null)
            {
                throw new EmployeeNotFoundException(request.Id);
            }

            return await Task.FromResult(new EmployeeDto
            {
                Id = employee.Id,
                RegistrationNumber = employee.RegistrationNumber.ToString(),
                Pesel = employee.Pesel,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                SecondName = employee.SecondName,
                DateOfBirth = employee.DateOfBirth,
                Gender = employee.Gender,
            });
        }
    }
}
