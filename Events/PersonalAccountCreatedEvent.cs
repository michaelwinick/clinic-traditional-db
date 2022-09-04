namespace Events;

public class PersonalAccountCreatedEvent
{
    public string AccountId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string SecurityQuestion { get; set; }
    public string SecurityAnswer { get; set; }
    public string HealthDataNotice { get; set; }
    public string TermsOfUse { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Dob { get; set; }

    public PersonalAccountCreatedEvent()
    {
        
    }

    public PersonalAccountCreatedEvent(string accountId, string email, string password, string securityQuestion, 
        string securityAnswer, string healthDataNotice, string termsOfUse, string firstName, string lastName, string dob)
    {
        AccountId = accountId;
        Email = email;
        Password = password;
        SecurityQuestion = securityQuestion;
        SecurityAnswer = securityAnswer;
        HealthDataNotice = healthDataNotice;
        TermsOfUse = termsOfUse;
        FirstName = firstName;
        LastName = lastName;
        Dob = dob;
    }
}