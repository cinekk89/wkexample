using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WKExample.Domain.Entities;
using WKExample.Domain.Repositories;

namespace WKExample.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employees = new List<Employee>()
            {
                new Employee(Guid.NewGuid(), new RegistrationNumber("1-000324"), "12340532", DateTime.Now.AddDays(-10), "Kowalski", "Jan", null, "Male", DateTime.Now),
                new Employee(Guid.NewGuid(), new RegistrationNumber("1-001234"), "51215464", DateTime.Now.AddDays(-3), "Małkowski", "Bartosz", "Jan", "Male", DateTime.Now),
                new Employee(Guid.NewGuid(), new RegistrationNumber("1-000004"), "65423425", DateTime.Now.AddDays(-24), "Nowak", "Anna", null, "Female", DateTime.Now),
                new Employee(Guid.NewGuid(), new RegistrationNumber("1-014144"), "76534353", DateTime.Now.AddDays(-51), "Jabłońska", "Zuzanna", "Anna", "Female", DateTime.Now),
            };

        public async Task Add(Employee employee)
        {
            _employees.Add(employee);
            await Task.FromResult(true);
        }

        public async Task<IEnumerable<Employee>> Get()
        {
            return await Task.FromResult(_employees);
        }

        public async Task<Employee> Get(Guid id)
        {
            var employees = await Get();

            return employees.SingleOrDefault(e => e.Id == id);
        }

        public async Task Remove(Guid id)
        {
            var employeeToRemove = _employees.SingleOrDefault(e => e.Id == id);
            await Task.FromResult(_employees.Remove(employeeToRemove));
        }

        public async Task Update(Guid id, Employee employee)
        {
            var employeeToUpdate = _employees.SingleOrDefault(e => e.Id == id);
            await Task.FromResult(true);
        }
    }
}
