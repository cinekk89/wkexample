using MediatR;
using System;
using WKExample.Application.DTOs;

namespace WKExample.Application.Queries
{
    public class GetEmployeeQuery : IRequest<EmployeeDto>
    {
        public Guid Id { get; set; }
    }
}
