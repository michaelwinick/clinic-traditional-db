namespace account_context.Dto;

public class CreateDependentAccountDto
{
    public string ParentId { get; set; }
    public string DependentFirstName { get; set; }
    public string Title { get; set; }   
    public string DependentLastName { get; set; }
    public string Month { get; set; }
    public string Day { get; set; }
    public string Year { get; set; }
}