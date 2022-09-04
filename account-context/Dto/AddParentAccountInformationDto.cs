namespace account_context.Dto;

public class AddParentAccountInformationDto
{
    public string ParentId { get; set; }

    public string ParentFirstName { get; set; }
    public string ParentLastName { get; set; }
    public string Month { get; set; }
    public string Day { get; set; }
    public string Year { get; set; }
    public string Country { get; set; }
    public string Region { get; set; }
}