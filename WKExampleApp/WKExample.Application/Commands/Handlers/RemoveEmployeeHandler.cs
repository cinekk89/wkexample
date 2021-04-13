using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WKExample.Domain.Exceptions;
using WKExample.Domain.Repositories;

namespace WKExample.Application.Commands.Handlers
{
    public class RemoveEmployeeHandler : INotificationHandler<RemoveEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public RemoveEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task Handle(RemoveEmployeeCommand notification, CancellationToken cancellationToken)
        {
            var employeeToRemove = _employeeRepository.Get(notification.Id);
            if (employeeToRemove is null)
            {
                throw new EmployeeNotFoundException(notification.Id);
            }

            await _employeeRepository.Remove(notification.Id);
        }
    }
}
