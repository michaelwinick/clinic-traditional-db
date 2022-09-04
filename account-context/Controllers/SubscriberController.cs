using System.Text.Json.Serialization;
using account_context.Domain.Commands;
using account_context.Dto;
using Dapr;
using Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace account_context.Controllers
{
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubscriberController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Topic("pubsub", "EmployeeAccountVerified")]
        [HttpPost("EmployeeAccountVerified")]
        public async Task<IActionResult> EmployeeAccountVerified(EmployeeAccountVerifiedEvent @event)
        {
            var result = await _mediator.Send(new MarkEmployeeVerifiedCommand(@event));

            return Ok();
        }

        [Topic("pubsub", "PersonalAccountCreated")]
        [HttpPost("PersonalAccountCreated")]
        public async Task<IActionResult> PersonalAccountCreated(PersonalAccountCreatedEvent @event)
        {
            var result = await _mediator.Send(new UpdatePersonalAccountCommand(@event));

            return Ok();
        }

        [Topic("pubsub", "DependentAccountCreated")]
        [HttpPost("DependentAccountCreated")]
        public async Task<IActionResult> DependentAccountCreated(DependentAccountCreatedEvent @event)
        {
            var result = await _mediator.Send(new UpdateDependentAccountCommand(@event));

            return Ok();
        }

        [Topic("pubsub", "ParentAccountCompleted")]
        [HttpPost("ParentAccountCompleted")]
        public async Task<IActionResult> ParentAccountCompleted(ParentAccountCompletedEvent @event)
        {
            var result = await _mediator.Send(new UpdateParentAccountCommand(@event));

            return Ok();
        }
    }
}

