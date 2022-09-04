using Dapr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InboundVerificationGateway.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InboundController : ControllerBase
{
    private readonly IMediator _mediator;

    public InboundController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost("MarkVerified")]
    public async Task<IActionResult> MarkVerified(MarkVerifiedDto dto)
    {
        var result = await _mediator.Send(new MarkVerifiedCommand(dto));

        return Ok();
    }
}