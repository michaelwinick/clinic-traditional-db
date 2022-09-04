namespace InboundVerificationGateway.Controllers;

public class MarkVerifiedDto
{
    public string AccountId { get; set; }
    public string Password { get; set; }
    public string SecurityQuestion { get; set; }
    public string SecurityAnswer { get; set; }
}