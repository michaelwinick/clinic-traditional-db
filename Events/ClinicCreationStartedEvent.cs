namespace Events;

public class ClinicCreationStartedEvent  
{
    public string AccountId { get; set; }
    public string ClinicId { get; set; }

    public ClinicCreationStartedEvent(string accountId, string clinicId)
    {
        AccountId = accountId;
        ClinicId = clinicId;
    }
}