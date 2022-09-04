using Dapr;
using Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pumper_context.Domain.Commands;
using pumper_context.DTO;

namespace pumper_context.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PumperController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PumperController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GiveAccessToHcp")]
        public async Task<IActionResult> GiveAccessToHcp(GiveAccessToHcpDto dto)
        {
            var result = await _mediator.Send(new GiveAccessToHcpCommand(dto.ClinicId, dto.PumperAccountId));

            return Ok(result);
        }

        [HttpPost("ApproveAccessToHcp")]
        public async Task<IActionResult> ApproveAccessToHcp(ApproveAccessToHcpCommand dto)
        {
            var result = await _mediator.Send(new ApproveAccessToHcpCommand(dto.ClinicId, dto.PumperAccountId));
            return Ok(result);
        }
    }
}
