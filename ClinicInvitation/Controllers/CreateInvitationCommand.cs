using MediatR;

namespace ClinicInvitation.Controllers;

public class CreateInvitationCommand : IRequest<bool>
{
    public CreateInvitationCommand(CreateInvitationDto dto)
    {
        InvitationId = dto.Invitationid;
    }

    public string InvitationId { get; set; }
}