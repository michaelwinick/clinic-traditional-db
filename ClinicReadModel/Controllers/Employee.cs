namespace ClinicReadModel.Controllers;

public class Employee
{
    public string EventAccountId { get; }

    public Employee()
    {
        
    }

    public Employee(string eventAccountId)
    {
        EventAccountId = eventAccountId;
    }
}