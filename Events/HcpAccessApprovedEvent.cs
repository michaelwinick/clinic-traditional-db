namespace Events;

public class HcpAccessApprovedEvent 
{
    public HcpAccessApprovedEvent()
    {
        
    }

    public HcpAccessApprovedEvent(string pumperId, string clinicId)
    {
        PumperId = pumperId;
        ClinicId = clinicId;
    }
    
    public string PumperId { get; set; }
    public string ClinicId { get; set; }
}