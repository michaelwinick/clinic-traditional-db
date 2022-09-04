namespace Events;

public class HcpAccessApprovedEvent
{
    public string PumperId { get; set; }
    public string HcpId { get; set; }

    public HcpAccessApprovedEvent(string pumperId, string hcpId)
    {
        PumperId = pumperId;
        HcpId = hcpId;
    }
}