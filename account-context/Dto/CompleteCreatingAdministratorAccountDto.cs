namespace account_context.Dto;

public class CompleteCreatingAdministratorAccountDto
{
    public string AccountId { get; set; }
    public string ClinicId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string SecurityQuestion { get; set; }
    public string SecurityAnswer { get; set; }
    public string Consent { get; set; }
}