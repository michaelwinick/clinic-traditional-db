namespace Events;

public class EmployeeAccountCreatedEvent  
{
    public string AccountId { get; set; }
    public string ClinicId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Title { get; set; }

  
    public EmployeeAccountCreatedEvent(
        string accountId, string clinicId, string email, string firstName, string lastName, string title)
    {
        AccountId = accountId;
        ClinicId = clinicId;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Title = title;
    }
}