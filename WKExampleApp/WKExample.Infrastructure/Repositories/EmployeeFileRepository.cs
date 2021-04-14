using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WKExample.Domain.Entities;
using WKExample.Domain.Repositories;
using WKExample.Shared.Providers;

namespace WKExample.Infrastructure.Repositories
{
    public class EmployeeFileRepository : IEmployeeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IDateTimeProvider _dateTimeProvider;

        const string DbFileLocationKey = "DbFileLocation";
        readonly string DbFilePath;

        public EmployeeFileRepository(IConfiguration configuration, IDateTimeProvider dateTimeProvider)
        {
            _configuration = configuration;
            _dateTimeProvider = dateTimeProvider;
            DbFilePath = _configuration[DbFileLocationKey];
        }

        public async Task Add(Employee employee)
        {
            var rows = await File.ReadAllLinesAsync(DbFilePath, Encoding.UTF8);
            var list = rows.ToList();

            var mappedEmployee = MapEmployee(employee);
            list.Add(mappedEmployee);

            await File.WriteAllLinesAsync(DbFilePath, list, Encoding.UTF8);
        }

        public async Task<IEnumerable<Employee>> Get()
        {
            var rows = await File.ReadAllLinesAsync(DbFilePath, Encoding.UTF8);
            var employees = new List<Employee>();

            if (rows.Any())
            {
                rows.ToList().ForEach(row =>
                {
                    var employeeData = row.Split(',');
                    if (employeeData.Length != 8)
                    {
                        throw new Exception("Data in db file are corrupted. Invalid number of columns.");
                    }

                    employees.Add(new Employee(
                        id: Guid.Parse(employeeData[0]),
                        registrationNumber: new RegistrationNumber(employeeData[1]),
                        pesel: employeeData[2],
                        dateOfBirth: DateTime.Parse(employeeData[3]),
                        lastName: employeeData[4],
                        firstName: employeeData[5],
                        secondName: employeeData[6],
                        gender: employeeData[7],
                        now: _dateTimeProvider.Now()));
                });
            }

            return employees;
        }

        public async Task<Employee> Get(Guid id)
        {
            var employees = await Get();
            return employees.SingleOrDefault(e => e.Id == id);
        }

        public async Task Remove(Guid id)
        {
            var employees = await Get();
            var list = employees.ToList();

            var employeeToRemove = list.Single(e => e.Id == id);
            list.Remove(employeeToRemove);

            await File.WriteAllLinesAsync(DbFilePath, list.Select(e => MapEmployee(e)), Encoding.UTF8);
        }

        public async Task Update(Guid id, Employee employee)
        {
            var employees = await Get();
            var list = employees.ToList();

            var employeeToUpdate = list.Single(e => e.Id == id);

            employeeToUpdate.SetPesel(employee.Pesel);
            employeeToUpdate.SetLastName(employee.LastName);
            employeeToUpdate.SetNames(employee.FirstName, employee.SecondName);
            employeeToUpdate.SetDateOfBirth(employee.DateOfBirth, _dateTimeProvider.Now());
            employeeToUpdate.SetGender(employee.Gender);

            await File.WriteAllLinesAsync(DbFilePath, list.Select(e => MapEmployee(e)), Encoding.UTF8);
        }

        private string MapEmployee(Employee employee)
        {
            return $"{employee.Id},{employee.RegistrationNumber.ToString()},{employee.Pesel},{employee.DateOfBirth.ToString("yyyy-MM-dd")},{employee.LastName},{employee.FirstName},{employee.SecondName},{employee.Gender}";
        }
    }
}
