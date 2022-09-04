namespace Events;

public class DependentCreatedEvent
{
    public string AccountId { get; set; }
    public string ParentId { get; set; }

    public DependentCreatedEvent(string accountId, string parentId)
    {
        AccountId = accountId;
        ParentId = parentId;
    }
}