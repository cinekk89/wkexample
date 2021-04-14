using System.Collections.Generic;
using System.Threading.Tasks;
using WKExample.Domain.Entities;

namespace WKExample.Domain.Repositories
{
    public interface IRegistrationNumberRepository
    {
        public Task<IEnumerable<RegistrationNumber>> Get();
        public Task<bool> IsAvailable(RegistrationNumber registrationNumber);
    }
}
