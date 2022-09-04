namespace Events;

public class ParentAccountCreationStartedEvent  
{
    public string AccountId { get; set; }

    public ParentAccountCreationStartedEvent(string accountId)
    {
        AccountId = accountId;
    }
}