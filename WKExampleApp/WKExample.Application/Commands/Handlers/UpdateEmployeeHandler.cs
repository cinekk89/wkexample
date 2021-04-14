using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WKExample.Domain.Exceptions.Employee;
using WKExample.Domain.Repositories;
using WKExample.Shared.Providers;

namespace WKExample.Application.Commands.Handlers
{
    public class UpdateEmployeeHandler : INotificationHandler<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UpdateEmployeeHandler(IEmployeeRepository employeeRepository, IDateTimeProvider dateTimeProvider)
        {
            _employeeRepository = employeeRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task Handle(UpdateEmployeeCommand notification, CancellationToken cancellationToken)
        {
            var employeeToUpdate = _employeeRepository.Get(notification.Id);
            if (employeeToUpdate is null)
            {
                throw new EmployeeNotFoundException(notification.Id);
            }

            employeeToUpdate.SetNames(notification.FirstName, notification.SecondName);
            employeeToUpdate.SetLastName(notification.LastName);
            employeeToUpdate.SetPesel(notification.Pesel);
            employeeToUpdate.SetGender(notification.Gender);
            employeeToUpdate.SetDateOfBirth(notification.DateOfBirth, _dateTimeProvider.Now());

            await _employeeRepository.Update(notification.Id, employeeToUpdate);
        }
    }
}
