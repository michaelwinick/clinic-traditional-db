namespace Events;

public class InvitationCreatedEvent
{
    public string InvitationId { get; set; }

    public InvitationCreatedEvent(string invitationId)
    {
        InvitationId = invitationId;
    }
}