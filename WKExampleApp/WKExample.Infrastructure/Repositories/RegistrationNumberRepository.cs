using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WKExample.Domain.Entities;
using WKExample.Domain.Repositories;

namespace WKExample.Infrastructure.Repositories
{
    public class RegistrationNumberRepository : IRegistrationNumberRepository
    {
        private readonly IEmployeeRepository employeeRepository;

        public RegistrationNumberRepository(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<RegistrationNumber>> Get()
        {
            var employees = await employeeRepository.Get();
            return employees.Select(e => e.RegistrationNumber);
        }

        public async Task<bool> IsAvailable(RegistrationNumber registrationNumber)
        {
            var unavailableRegistrationNumbers = await Get();
            return !unavailableRegistrationNumbers.Any(rn => rn.ToString() == registrationNumber.ToString());
        }
    }
}
