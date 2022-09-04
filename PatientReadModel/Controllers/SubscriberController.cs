using Dapr;
using Dapr.Client;
using Events;
using Microsoft.AspNetCore.Mvc;

namespace AccountsReadModel.Controllers;

[ApiController]
public class SubscriberController : ControllerBase
{
    private const string DaprStoreName = "statestore";
    private readonly DaprClient _daprClient;

    public SubscriberController(DaprClient daprClient)
    {
        _daprClient = daprClient;
    }

    [Topic("pubsub", "PersonalAccountCreated")]
    [HttpPost("PersonalAccountCreated")]
    public async Task<IActionResult> PersonalAccountCreated(PersonalAccountCreatedEvent @event)
    {
        var stateAsync = await _daprClient.GetStateAsync<AccountsReadModel>(DaprStoreName, @event.AccountId) ??
                         new AccountsReadModel(@event.AccountId);

        stateAsync.FirstName = @event.FirstName;
        stateAsync.LastName = @event.LastName;
        stateAsync.Email = @event.Email;
        stateAsync.Dob = @event.Dob;

        await _daprClient.SaveStateAsync(DaprStoreName, @event.AccountId, stateAsync);

        return Ok();
    }

    [Topic("pubsub", "ParentAccountCompleted")]
    [HttpPost("ParentAccountCompleted")]
    public async Task<IActionResult> ParentAccountCompleted(ParentAccountCompletedEvent @event)
    {
        var stateAsync = await _daprClient.GetStateAsync<AccountsReadModel>(DaprStoreName, @event.DependentId) ??
                         new AccountsReadModel(@event.DependentId);

        stateAsync.CareTakerName = @event.CareTakerName;
        stateAsync.Email = @event.Email;

        await _daprClient.SaveStateAsync(DaprStoreName, @event.DependentId, stateAsync);

        return Ok();
    }

    [Topic("pubsub", "DependentAccountCreated")]
    [HttpPost("DependentAccountCreated")]
    public async Task<IActionResult> DependentAccountCreated(DependentAccountCreatedEvent @event)
    {
        var stateAsync = await _daprClient.GetStateAsync<AccountsReadModel>(DaprStoreName, @event.AccountId) ??
                         new AccountsReadModel(@event.AccountId);

        stateAsync.FirstName = @event.DependentFirstName;
        stateAsync.LastName = @event.DependentLastName;
        stateAsync.Dob = @event.Month + "/" + @event.Day + "/" + @event.Year;
        stateAsync.CareTaker = @event.ParentId;

        await _daprClient.SaveStateAsync(DaprStoreName, @event.AccountId, stateAsync);

        return Ok();
    }

}