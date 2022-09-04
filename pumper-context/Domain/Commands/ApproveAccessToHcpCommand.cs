using MediatR;

namespace pumper_context.Domain.Commands;

public class ApproveAccessToHcpCommand : IRequest<bool>
{
    public string ClinicId { get; set; }
    public string PumperAccountId { get; set; }

    public ApproveAccessToHcpCommand(string clinicId, string pumperAccountId)
    {
        ClinicId = clinicId;
        PumperAccountId = pumperAccountId;
    }
}