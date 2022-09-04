using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace ClinicInvitationReadModel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClinicInvitationController : ControllerBase
    {
        private const string DaprStoreName = "statestore";

        private readonly DaprClient _daprClient;

        public ClinicInvitationController(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        [HttpGet]
        [Route("IsInvitionValid/{invitationId}")]
        public async Task<ActionResult<bool>> IsInvitationValid(string invitationId)
        {
            var account = await _daprClient.GetStateAsync<Invitation>(DaprStoreName, invitationId);
            if (account == null) return Ok(false);

            return Ok(true);    
        }
    }
}
