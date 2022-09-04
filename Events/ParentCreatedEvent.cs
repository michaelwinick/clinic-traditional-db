namespace Events;

public class ParentCreatedEvent
{
    public string AccountId { get; set; }
    public string DependentId { get; set; }

    public ParentCreatedEvent(string accountId, string dependentId)
    {
        AccountId = accountId;
        DependentId = dependentId;
    }
}