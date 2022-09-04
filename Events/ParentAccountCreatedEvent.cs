namespace Events;

public class ParentAccountCreatedEvent
{
    public string AccountId { get; set; }
    public string DependentId { get; set; }

    public ParentAccountCreatedEvent()
    {
        
    }

    public ParentAccountCreatedEvent(string accountId, string dependentId)
    {
        AccountId = accountId;
        DependentId = dependentId;
    }
}