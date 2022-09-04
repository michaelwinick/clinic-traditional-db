using MediatR;

namespace clinic_context.Domain.Commands;

public class RequestAccessToReportsCommand : IRequest<bool>
{
    public string ClinicId { get; set; }
    public string PumperAccountId { get; set; }

    public RequestAccessToReportsCommand(string pumperAccountId, string clinicId)
    {
        ClinicId = clinicId;
        PumperAccountId = pumperAccountId;
    }
}