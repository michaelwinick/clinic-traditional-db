namespace Events;

public class PumperAccessAcknowledged
{
    public string PumperId { get; set; }
    public string ClinicId { get; set; }

    public PumperAccessAcknowledged(string pumperId, string clinicId)
    {
        PumperId = pumperId;
        ClinicId = clinicId;
    }
}