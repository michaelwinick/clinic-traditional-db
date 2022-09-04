namespace Events;

public class PumperAccessApproved
{
    public string PumperId { get; set; }
    public string HcpId { get; set; }

    public PumperAccessApproved(string pumperId, string hcpId)
    {
        PumperId = pumperId;
        HcpId = hcpId;
    }
}