using System.Collections.Generic;
using System.Threading.Tasks;
using WKExample.Domain.Entities;

namespace WKExample.Domain.Repositories
{
    public interface IRegistrationNumberRepository
    {
        public IEnumerable<RegistrationNumber> Get();
        public bool IsAvailable(RegistrationNumber registrationNumber);
    }
}
