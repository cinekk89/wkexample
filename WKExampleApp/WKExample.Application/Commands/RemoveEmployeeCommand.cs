using MediatR;
using System;

namespace WKExample.Application.Commands
{
    public class RemoveEmployeeCommand : INotification
    {
        public Guid Id { get; set; }
    }
}
