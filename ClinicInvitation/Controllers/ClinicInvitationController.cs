using Dapr;
using Dapr.Client;
using Events;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicInvitation.Controllers;

[ApiController]
[Route("[controller]")]
public class ClinicInvitationController : ControllerBase
{
    private const string DaprStoreName = "statestore";
    private readonly IMediator _mediator;
    private readonly DaprClient _daprClient;

    public ClinicInvitationController(IMediator mediator, DaprClient daprClient)
    {
        _mediator = mediator;
        _daprClient = daprClient;
    }

    [HttpPost]
    [Route("createInvitation")]
    public async Task<IActionResult> CreateInvitation([FromBody] CreateInvitationDto dto)
    {
        var result = await _mediator.Send(new CreateInvitationCommand(dto));

        return Ok(result);
    }

    [Topic("pubsub", "InvitationCreated")]
    [HttpPost("InvitationLoaded")]
    public async Task<IActionResult> InvitationCreated(InvitationCreatedEvent @event)
    {
        await _daprClient.SaveStateAsync(DaprStoreName, @event.InvitationId, new Invitation(@event.InvitationId));

        return Ok();
    }
}