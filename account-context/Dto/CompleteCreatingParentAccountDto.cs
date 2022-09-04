namespace account_context.Dto;

public class CompleteCreatingParentAccountDto
{
    public string ParentId { get; set; }
    public string DependentId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string SecurityQuestion { get; set; }
    public string SecurityAnswer { get; set; }
    public string ParentConfirmation { get; set; } 
    public string HealthDataConfirmation { get; set; } 
    public string TermsOfService { get; set; } 
}