namespace Events;

public class ParentAccountInformationAddedEvent
{
    public string AccountId { get; set; }
    public string FirstName { get; set; }

    public ParentAccountInformationAddedEvent(string accountId)
    {
        AccountId = accountId;
    }
}