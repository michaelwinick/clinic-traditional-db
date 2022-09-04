namespace account_context.Dto;

public class CompletePersonalAccountDto
{
    public string AccountId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string SecurityQuestion { get; set; }
    public string SecurityAnswer { get; set; }
    public string HealthDataNotice { get; set; }
    public string TermsOfUse { get; set; }
}
