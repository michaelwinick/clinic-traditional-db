namespace Events;

public class AdministratorAccountCreatedEvent
{
    public string AccountId { get; set; }
    public string ClinicId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Title { get; set; }

    public AdministratorAccountCreatedEvent()
    {
        
    }

    public AdministratorAccountCreatedEvent(string accountId, string clinicId, string email, string firstName, string lastName, string title)
    {
        AccountId = accountId;
        ClinicId = clinicId;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Title = title;
    }
}