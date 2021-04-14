using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WKExample.Domain.Entities;

namespace WKExample.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> Get();
        public Task<Employee> Get(Guid id);
        public Task Add(Employee employee);
        public Task Update(Guid id, Employee employee);
        public Task Remove(Guid id);

    }
}
