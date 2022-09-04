using MediatR;

namespace pumper_context.Domain.Commands;

public class RequestAccessToReportsCommand : IRequest<bool>
{
    public string ClinicId { get; set; }
    public string PumperAccountId { get; set; }

    public RequestAccessToReportsCommand(string clinicId, string pumperAccountId)
    {
        ClinicId = clinicId;
        PumperAccountId = pumperAccountId;
    }
}