using System.Text.Json;
using Dapr;
using Dapr.Client;
using Events;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ClinicReadModel.Controllers;

[ApiController]
public class SubscriberController : ControllerBase
{
    private const string DaprStoreName = "statestore";
    private readonly DaprClient _daprClient;

    public SubscriberController(DaprClient daprClient)
    {
        _daprClient = daprClient;
    }

    [Topic("pubsub", "ClinicAccountCreated")]
    [HttpPost("ClinicAccountCreated")]
    public async Task<IActionResult> ClinicAccountCreated(ClinicAccountCreatedEvent @event)
    {
        var stateAsync = await _daprClient.GetStateAsync<ClinicReadModel>(DaprStoreName, @event.ClinicId) ?? 
                         new ClinicReadModel(@event.ClinicId);

        stateAsync.CompanyName = @event.CompanyName;
        stateAsync.Address = @event.Address;
        stateAsync.Country = @event.Country;

        await _daprClient.SaveStateAsync(DaprStoreName, @event.ClinicId, stateAsync);

        return Ok();
    }

    [Topic("pubsub", "AdministratorAccountCreated")]
    [HttpPost("AdministratorAccountCreated")]
    public async Task<IActionResult> AdministratorAccountCreated(AdministratorAccountCreatedEvent @event)
    {
        var stateAsync = await _daprClient.GetStateAsync<ClinicReadModel>(DaprStoreName, @event.ClinicId) ?? 
                              new ClinicReadModel(@event.ClinicId);

        stateAsync.ProfessionalAccounts.Add(
            new Admin(@event.AccountId, "Admin", @event.Email, @event.FirstName, @event.LastName, @event.Title));

        await _daprClient.SaveStateAsync(DaprStoreName, @event.ClinicId, stateAsync);

        return Ok();
    }

    [Topic("pubsub", "EmployeeAccountCreated")]
    [HttpPost("EmployeeAccountCreated")]
    public async Task<IActionResult> EmployeeAccountCreated(EmployeeAccountCreatedEvent @event)
    {
        var stateAsync = await _daprClient.GetStateAsync<ClinicReadModel>(DaprStoreName, @event.ClinicId) ?? 
                         new ClinicReadModel(@event.ClinicId);

        stateAsync.ProfessionalAccounts.Add(
            new Admin(@event.AccountId, "Standard", @event.Email, @event.FirstName, @event.LastName, @event.Title));

        await _daprClient.SaveStateAsync(DaprStoreName, @event.ClinicId, stateAsync);

        return Ok();
    }

    [Topic("pubsub", "ClinicAccessApproved")]
    [HttpPost("ClinicAccessApproved")]
    public async Task<IActionResult> ClinicAccessApproved(ClinicAccessApprovedEvent @event)
    {
        var stateAsync = await _daprClient.GetStateAsync<ClinicReadModel>(DaprStoreName, @event.ClinicId) ?? 
                         new ClinicReadModel(@event.ClinicId);

        var httpClient = new HttpClient();
        var result = await httpClient.GetAsync($"https://localhost:7065/api/Accounts/GetAccount/{@event.AccountId}");
        if (!result.IsSuccessStatusCode) return Ok();
        var jsonDocument = JObject.Parse(await result.Content.ReadAsStringAsync());

        var patient = new Patient(
            @event.AccountId,
            (string)jsonDocument["FirstName"]!,
            (string)jsonDocument["LastName"]!,
            (string)jsonDocument["Email"]!,
            (string)jsonDocument["Dob"]!,
            (string)jsonDocument["CareTakerName"]!);

        stateAsync.Patients.Add(patient);
        await _daprClient.SaveStateAsync(DaprStoreName, @event.ClinicId, stateAsync);

        return Ok();
    }
}