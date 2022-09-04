using Dapr;
using Dapr.Client;
using Events;
using Microsoft.AspNetCore.Mvc;

namespace ClinicInvitationReadModel.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriberController : ControllerBase
{
    private const string DaprStoreName = "statestore";

    private readonly DaprClient _daprClient;

    public SubscriberController(DaprClient daprClient)
    {
        _daprClient = daprClient;
    }

    [Topic("pubsub", "InvitationCreated")]
    [HttpPost("InvitationCreated")]
    public async Task<IActionResult> InvitationCreated(InvitationCreatedEvent @event)
    {
        await _daprClient.SaveStateAsync(DaprStoreName, @event.InvitationId, new Invitation(@event.InvitationId));

        return Ok();
    }
}