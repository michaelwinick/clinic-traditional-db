using System.Text.Json.Serialization;

namespace ClinicInvitation.Controllers;

public class Invitation
{
    public Invitation(string invitationId)
    {
        InvitationId = invitationId;
    }

    [JsonPropertyName("invitationId")]
    public string InvitationId { get; set; }
}