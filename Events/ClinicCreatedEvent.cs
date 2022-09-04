namespace Events;

public class ClinicCreatedEvent  
{
    public string ClinicId { get; set; }
    public string AccountId { get; set; }

    public ClinicCreatedEvent(string clinicId, string accountId)
    {
        ClinicId = clinicId;
        AccountId = accountId;
    }
}