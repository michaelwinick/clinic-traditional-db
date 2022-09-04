namespace Events;

public class DependentAccountCreatedEvent
{
    public string AccountId { get; set; }
    public string ParentId { get; set; }
    public string DependentFirstName { get; set; }
    public string Title { get; set; }
    public string DependentLastName { get; set; }
    public string Month { get; set; }
    public string Day { get; set; }
    public string Year { get; set; }

    public DependentAccountCreatedEvent()
    {
        
    }

    public DependentAccountCreatedEvent(string accountId, string parentId, string dependentFirstName, string title, 
        string dependentLastName, string month, string day, string year)
    {
        AccountId = accountId;
        ParentId = parentId;
        DependentFirstName = dependentFirstName;
        Title = title;
        DependentLastName = dependentLastName;
        Month = month;
        Day = day;
        Year = year;
    }
}