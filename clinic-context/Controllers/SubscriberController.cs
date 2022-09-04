using clinic_context.Domain.Commands;
using Dapr;
using Events;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace clinic_context.Controllers
{
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubscriberController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Topic("pubsub", "ClinicAccountCreated")]
        [HttpPost("ClinicAccountCreated")]
        public async Task<IActionResult> ClinicAccountCreated(ClinicAccountCreatedEvent @event)
        {
            var result = await _mediator.Send(new CreateClinicCommand(@event.ClinicId, @event.AccountId));

            return Ok();
        }

        [Topic("pubsub", "AdministratorAccountCreated")]
        [HttpPost("AdministratorAccountCreated")]
        public async Task<IActionResult> AdministratorAccountCreated(AdministratorAccountCreatedEvent @event)
        {
            var result = await _mediator.Send(new CreateAdministratorCommand(@event.AccountId, @event.ClinicId));

            return Ok();
        }

        [Topic("pubsub", "EmployeeAccountCreated")]
        [HttpPost("EmployeeAccountCreated")]
        public async Task<IActionResult> EmployeeAccountCreated(EmployeeAccountCreatedEvent @event)
        {
            var result = await _mediator.Send(new CreateEmployeeCommand(@event.AccountId, @event.ClinicId));

            return Ok();
        }

        [Topic("pubsub", "HcpAccessApproved")]
        [HttpPost("HcpAccessApproved")]
        public async Task<IActionResult> HcpAccessApproved(HcpAccessApprovedEvent @event)
        {
            var result = await _mediator.Send(new ApproveClinicCommand(@event.PumperId, @event.ClinicId));

            return Ok();
        }
    }
}

