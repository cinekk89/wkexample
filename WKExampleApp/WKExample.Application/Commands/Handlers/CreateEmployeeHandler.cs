using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WKExample.Domain.Entities;
using WKExample.Domain.Exceptions.Employee;
using WKExample.Domain.Repositories;
using WKExample.Shared.Providers;

namespace WKExample.Application.Commands.Handlers
{
    public sealed class CreateEmployeeHandler : INotificationHandler<CreateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRegistrationNumberRepository _registrationNumberRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateEmployeeHandler(IEmployeeRepository employeeRepository, IRegistrationNumberRepository registrationNumberRepository, IDateTimeProvider dateTimeProvider)
        {
            _employeeRepository = employeeRepository;
            _registrationNumberRepository = registrationNumberRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task Handle(CreateEmployeeCommand notification, CancellationToken cancellationToken)
        {
            var employee = _employeeRepository.Get()
                .SingleOrDefault(e => e.Pesel == notification.Pesel);
            if (employee != null)
            {
                throw new EmployeeWithPeselAlreadyExistsException(notification.Pesel);
            }

            var unavailableRegistrationNumbers = _registrationNumberRepository.Get();
            var newEmployee = Employee.Create(
                notification.Pesel,
                notification.DateOfBirth,
                notification.LastName,
                notification.FirstName,
                notification.SecondName,
                notification.Gender,
                _dateTimeProvider.Now(),
                unavailableRegistrationNumbers
                );

            await _employeeRepository.Add(newEmployee);
            //for the future -> place for dispatching aggregateEvents
        }
    }
}
