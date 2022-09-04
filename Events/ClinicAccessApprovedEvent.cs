namespace Events;

public class ClinicAccessApprovedEvent 
{
    public string ClinicId { get; set; }
    public string AccountId { get; set; }

    public ClinicAccessApprovedEvent()
    {
        
    }

    public ClinicAccessApprovedEvent(string clinicId, string accountId)
    {
        ClinicId = clinicId;
        AccountId = accountId;
    }
}