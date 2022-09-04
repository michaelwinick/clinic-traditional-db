namespace Events;

public class EmployeeUnverifiedAccountCreatedEvent
{
    public string AccountId { get; set; }
    public string ClinicId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public EmployeeUnverifiedAccountCreatedEvent(
        string accountId, string clinicId, string firstName, string lastName, string email)
    {
        AccountId = accountId;
        ClinicId = clinicId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
}