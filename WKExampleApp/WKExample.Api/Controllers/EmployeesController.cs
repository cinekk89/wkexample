using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WKExample.Application.Commands;
using WKExample.Application.DTOs;
using WKExample.Application.Queries;

namespace WKExample.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly IMediator _mediator;

        public EmployeesController(ILogger<EmployeesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get()
        {

            return Ok(await _mediator.Send(new GetAllEmployeesQuery()));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeDto>> Get(Guid id)
        {
            return Ok(await _mediator.Send(new GetEmployeeQuery { Id = id }));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _mediator.Publish(new RemoveEmployeeCommand { Id = id });
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]CreateEmployeeCommand command)
        {
            await _mediator.Publish(command);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody]UpdateEmployeeCommand command)
        {
            await _mediator.Publish(command);
            return Ok();
        }
    }
}
