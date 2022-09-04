namespace Events;

public class ClinicAccessAcknowledged
{
    public string PumperId { get; set; }
    public string HcpId { get; set; }

    public ClinicAccessAcknowledged(string pumperId, string hcpId)
    {
        PumperId = pumperId;
        HcpId = hcpId;
    }
}