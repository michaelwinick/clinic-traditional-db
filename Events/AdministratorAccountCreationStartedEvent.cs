namespace Events;

public class AdministratorAccountCreationStartedEvent  
{
    public string AccountId { get; set; }

    public AdministratorAccountCreationStartedEvent(string accountId)
    {
        AccountId = accountId;
    }
}