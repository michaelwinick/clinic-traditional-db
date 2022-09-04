namespace Events;

public class PersonalAccountInformationAddedEvent 
{
    public string AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Dob { get; set; }

    public PersonalAccountInformationAddedEvent()
    {
        
    }

    public PersonalAccountInformationAddedEvent(string accountId, string firstName, string lastName, string dob)
    {
        AccountId = accountId;
        FirstName = firstName;
        LastName = lastName;
        Dob = dob;
    }
}