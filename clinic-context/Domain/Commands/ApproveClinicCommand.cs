using MediatR;

namespace clinic_context.Domain.Commands;

public class ApproveClinicCommand : IRequest<bool>
{
    public string PatientId { get; set; }
    public string ClinicId { get; set; }


    public ApproveClinicCommand(string patientId, string clinicId)
    {
        PatientId = patientId;
        ClinicId = clinicId;
    }
}