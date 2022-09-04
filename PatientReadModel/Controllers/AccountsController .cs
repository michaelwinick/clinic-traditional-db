using System.Text.Json;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace AccountsReadModel.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AccountsController : ControllerBase
{
    private const string DaprStoreName = "statestore";
    private readonly DaprClient _daprClient;

    public AccountsController(DaprClient daprClient)
    {
        _daprClient = daprClient;
    }

    [HttpGet("GetAccount/{accountId}")]
    public async Task<IActionResult> GetAccount(string accountId)
    {
        var stateAsync = await _daprClient.GetStateAsync<AccountsReadModel>(DaprStoreName, accountId);
        if (stateAsync == null) return NotFound(accountId);

        var serialize = JsonSerializer.Serialize(stateAsync);

        return Ok(serialize);
    }
}