using account_context.Domain.Aggregates;
using account_context.Domain.Commands;
using account_context.Dto;
using Dapr.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace account_context.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly DaprClient _daprClient;

        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        public AccountsController(IMediator mediator, DaprClient daprClient)
        {
            _mediator = mediator;
            _daprClient = daprClient;
        }

        [HttpPost]
        [Route("personal/startCreatingPersonalAccount")]
        public async Task<IActionResult> StartCreatingPersonalAccount([FromBody] StartCreatingPersonalAccountDto startDto)
        {
            var result = await _mediator.Send(new StartCreatingPersonalAccountCommand(startDto));

            return Ok(result);
        }

        [HttpPost]
        [Route("personal/addPersonalAccountInformation")]
        public async Task<IActionResult> AddPersonalAccountInformation([FromBody] AddPersonalAccountInformationDto addDto)
        {
            var result = await _mediator.Send(new AddPersonalAccountInformationCommand(addDto));
            return result ? Ok(result) : BadRequest();
        }

        [HttpPost]
        [Route("personal/completePersonalAccount")]
        public async Task<IActionResult> CompletePersonalAccount([FromBody] CompletePersonalAccountDto completeDto)
        {
            var result = await _mediator.Send(new CompletePersonalAccountCommand(completeDto));
            return result ? Ok(result) : BadRequest();
        }

        [HttpPost]
        [Route("personal/startCreatingParentAccount")]
        public async Task<IActionResult> StartCreatingParentAccount([FromBody] StartCreatingParentAccountDto startDto)
        {
            var result = await _mediator.Send(new StartCreatingParentAccountCommand(startDto));

            return Ok(result);
        }

        [HttpPost]
        [Route("personal/addParentAccountInformation")]
        public async Task<IActionResult> AddParentAccountInformation([FromBody] AddParentAccountInformationDto addDto)
        {
            var result = await _mediator.Send(new AddParentAccountInformationCommand(addDto));
            return result ? Ok(result) : BadRequest();
        }

        [HttpPost]
        [Route("personal/createDependentAccount")]
        public async Task<IActionResult> CreateDependentAccount([FromBody] CreateDependentAccountDto completeDto)
        {
            var result = await _mediator.Send(new CreateDependentAccountCommand(completeDto));
            return Ok(result);
        }

        [HttpPost]
        [Route("personal/completeCreatingParentAccount")]
        public async Task<IActionResult> CompleteCreatingParentAccount([FromBody] CompleteCreatingParentAccountDto completeDto)
        {
            var result = await _mediator.Send(new CompleteCreatingParentAccountCommand(completeDto));
            return result ? Ok(result) : BadRequest();
        }
        

        [HttpPost]
        [Route("professional/startCreatingClinicAccount")]
        public async Task<IActionResult> StartCreatingClinicAccount([FromBody] StartCreatingClinicAccountDto dto)
        {
            var result = await _mediator.Send(new StartCreatingClinicAccountCommand(dto));

            return Ok(result);
        }

        [HttpPost]
        [Route("professional/completeCreatingClinicAccount")]
        public async Task<IActionResult> CompleteCreatingClinicAccount([FromBody] CompleteCreatingClinicAccountDto accountDto)
        {
            var result = await _mediator.Send(new CompleteCreatingClinicAccountCommand(accountDto));

            return Ok(result);
        }


        [HttpPost]
        [Route("professional/startCreatingAdministratorAccount")]
        public async Task<IActionResult> StartCreatingAdministratorAccount([FromBody] StartCreatingAdministratorAccountDto dto)
        {
            var result = await _mediator.Send(new StartCreatingAdministratorAccountCommand(dto));

            return Ok(result);
        }

        [HttpPost]
        [Route("professional/completeCreatingAdministratorAccount")]
        public async Task<IActionResult> CompleteCreatingAdministratorAccount([FromBody] CompleteCreatingAdministratorAccountDto dto)
        {
            var result = await _mediator.Send(new CompleteCreatingAdministratorAccountCommand(dto));

            return Ok(result);
        }


        [HttpPost]
        [Route("professional/startCreatingEmployeeAccount")]
        public async Task<IActionResult> StartCreatingEmployeeAccount([FromBody] StartCreatingEmployeeAccountDto dto)
        {
            var result = await _mediator.Send(new StartCreatingEmployeeAccountCommand(dto));

            return Ok(result);
        }

        [HttpGet]
        [Route("professional/isEmployeeVerified/{accountId}")]
        public async Task<IActionResult> IsEmployeeVerified(string accountId)
        {
            var account = await _daprClient.GetStateAsync<Account>(DaprStoreName, accountId);
            if (account == null || account.IsVerified == false) return NotFound(accountId);

            return Ok();
        }

    }
}
