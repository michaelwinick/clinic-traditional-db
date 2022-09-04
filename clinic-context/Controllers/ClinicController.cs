using clinic_context.Domain.Commands;
using clinic_context.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace clinic_context.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClinicController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("RequestAccessToReports")]
        public async Task<IActionResult> RequestAccessToReports(RequestAccessToReportsDto dto)
        {
            var result = await _mediator.Send(new RequestAccessToReportsCommand(dto.PatientId, dto.ClinicId));

            return Ok(result);
        }
    }
}
