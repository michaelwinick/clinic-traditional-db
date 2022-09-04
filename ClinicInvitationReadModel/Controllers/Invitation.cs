namespace ClinicInvitationReadModel.Controllers;

public class Invitation
{
    public Invitation(string invitationId)
    {
        InvitationId = invitationId;
    }

    public string InvitationId { get; set; }
}