namespace Events;

public class PumperCreatedEvent 
{
    public string DependentId { get; set; }
    public string AccountId { get; set; }

    public PumperCreatedEvent()
    {
        
    }
    public PumperCreatedEvent(string accountId)
    {
        AccountId = accountId;
        DependentId = accountId;
    }

    public PumperCreatedEvent(string dependentId, string accountId)
    {
        DependentId = dependentId;
        AccountId = accountId;
    }
}