namespace Events;

public class PumperAccessRequestedEvent
{
    public string PumperId { get; set; }
    public string HcpId { get; set; }

    public PumperAccessRequestedEvent(string pumperId, string hcpId)
    {
        PumperId = pumperId;
        HcpId = hcpId;
    }
}