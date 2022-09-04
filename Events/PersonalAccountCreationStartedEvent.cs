namespace Events;

public class PersonalAccountCreationStartedEvent 
{
    public string NewAccountId { get; set; }
    public string RequestInvitation { get; set; }

    public PersonalAccountCreationStartedEvent(string newAccountId, string requestInvitation)
    {
        NewAccountId = newAccountId;
        RequestInvitation = requestInvitation;
    }
}