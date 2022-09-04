using MediatR;

namespace pumper_context.Domain.Commands;

public class GiveAccessToHcpCommand : IRequest<bool>
{
    public string PumperAccountId { get; set; }
    public string ClinicId { get; set; }

    public GiveAccessToHcpCommand(string clinicId, string pumperAccountId)
    {
        ClinicId = clinicId;
        PumperAccountId = pumperAccountId;
    }
}