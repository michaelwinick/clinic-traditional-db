namespace Events;

public class EmployeeAccountCreationStartedEvent  
{
    public string AccountId { get; set; }

    public EmployeeAccountCreationStartedEvent(string accountId)
    {
        AccountId = accountId;
    }
}