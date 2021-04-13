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

        public IEnumerable<RegistrationNumber> Get()
        {
            return employeeRepository.Get().Select(e => e.RegistrationNumber);
        }

        public bool IsAvailable(RegistrationNumber registrationNumber)
        {
            var unavailableRegistrationNumbers = employeeRepository.Get().Select(e => e.RegistrationNumber);
            return !unavailableRegistrationNumbers.Any(rn => rn.ToString() == registrationNumber.ToString());
        }
    }
}
