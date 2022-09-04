    namespace account_context.Dto;

public class StartCreatingAdministratorAccountDto
{
    public string ClinicId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Title { get; set; }
    public string DefaultLanguage { get; set; }
}