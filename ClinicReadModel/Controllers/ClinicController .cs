using System.Text.Json;
using Dapr;
using Dapr.Client;
using Events;
using Microsoft.AspNetCore.Mvc;

namespace ClinicReadModel.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ClinicController : ControllerBase
{
    private const string DaprStoreName = "statestore";
    private readonly DaprClient _daprClient;

    public ClinicController(DaprClient daprClient)
    {
        _daprClient = daprClient;
    }

    [HttpGet("GetClinic/{clinicId}")]
    public async Task<IActionResult> GetClinic(string clinicId)
    {
        var stateAsync = await _daprClient.GetStateAsync<ClinicReadModel>(DaprStoreName, clinicId);
        if (stateAsync == null) return NotFound(clinicId);

        var serialize = JsonSerializer.Serialize(stateAsync);

        return Ok(serialize);
    }
}