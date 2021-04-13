using System;
using System.Collections.Generic;
using System.Text;

namespace WKExample.Application.DTOs
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Pesel { get; set; }
        public string RegistrationNumber { get; set; }
        public string Gender { get; set; }
    }
}
