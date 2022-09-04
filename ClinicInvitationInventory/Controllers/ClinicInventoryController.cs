using Microsoft.AspNetCore.Mvc;

namespace ClinicInvitationInventory.Controllers;

[ApiController]
[Route("[controller]")]
public class ClinicInventoryController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<ClinicInventoryController> _logger;

    public ClinicInventoryController(ILogger<ClinicInventoryController> logger)
    {
        _logger = logger;
    }

    
}
