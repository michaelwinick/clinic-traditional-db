using Dapr;
using Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pumper_context.Domain.Commands;
using pumper_context.Domain.Handlers;

namespace pumper_context.Controllers
{
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubscriberController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Topic("pubsub", "ParentAccountCompleted")]
        [HttpPost("ParentAccountCompleted")]
        public async Task<IActionResult> ParentAccountCreated(ParentAccountCompletedEvent @event)
        {
            await _mediator.Send(new CreateParentCommand(@event.ParentId, @event.DependentId));

            return Ok();
        }

        [Topic("pubsub", "DependentAccountCreated")]
        [HttpPost("DependentAccountCreated")]
        public async Task<IActionResult> DependentAccountCreated(DependentAccountCreatedEvent @event)
        {
            await _mediator.Send(new CreateDependentCommand(@event.AccountId, @event.ParentId));

            return Ok();
        }

        [Topic("pubsub", "PersonalAccountCreated")]
        [HttpPost("PersonalAccountCreated")]
        public async Task<IActionResult> PumperCreated(PersonalAccountCreatedEvent @event)
        {
            await _mediator.Send(new UpdatePumperCommand(@event));

            return Ok();
        }

        [Topic("pubsub", "DependentCreated")]
        [HttpPost("DependentCreated")]
        public async Task<IActionResult> DependentCreated(DependentCreatedEvent @event)
        {
            await _mediator.Send(new UpdateDependentCommand(@event));

            return Ok();
        }

        [Topic("pubsub", "ParentCreated")]
        [HttpPost("ParentCreated")]
        public async Task<IActionResult> ParentCreated(ParentCreatedEvent @event)
        {
            await _mediator.Send(new UpdateParentCommand(@event));

            return Ok();
        }

        [Topic("pubsub", "PatientAccessRequested")]
        [HttpPost("PatientAccessRequested")]
        public async Task<IActionResult> PatientAccessRequested(PatientAccessRequestedEvent @event)
        {
            await _mediator.Send(new PatientAccessRequestedCommand(@event));

            return Ok();
        }
    }
}
