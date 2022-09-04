using Dapr;
using Events;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OutboundVerificationGateway.Domain;

namespace OutboundVerificationGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class OutboundVerificationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<OutboundVerificationController> logger;

    public OutboundVerificationController(IMediator mediator, ILogger<OutboundVerificationController> logger)
    {
        _mediator = mediator;
        this.logger = logger;
    }

    [Topic("pubsub", "EmployeeUnverifiedAccountCreated")]
    [HttpPost("EmployeeUnverifiedAccountCreated")]
    public async Task<IActionResult> EmployeeUnverifiedAccountCreated(EmployeeUnverifiedAccountCreatedEvent @event)
    {
        var result = await _mediator.Send(new SendVerificationEmailCommand(
            @event.AccountId,
            @event.Email));

            logger.LogInformation($"EmployeeUnverifiedAccountCreated for account: {@event.AccountId}");

        return Ok();
    }
}