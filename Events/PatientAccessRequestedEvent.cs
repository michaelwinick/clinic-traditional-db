namespace Events;

public class PatientAccessRequestedEvent  
{
    public string PatientId { get; set; }
    public string ClinicId { get; set; }

    public PatientAccessRequestedEvent(string patientId, string clinicId)
    {
        PatientId = patientId;
        ClinicId = clinicId;
    }
}