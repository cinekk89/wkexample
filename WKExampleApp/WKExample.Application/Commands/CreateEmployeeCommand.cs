using MediatR;
using System;

namespace WKExample.Application.Commands
{
    public class CreateEmployeeCommand : INotification
    {
        public string Pesel { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Gender { get; set; }
    }
}
