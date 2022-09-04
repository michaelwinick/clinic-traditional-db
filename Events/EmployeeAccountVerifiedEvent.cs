namespace Events;

public class EmployeeAccountVerifiedEvent 
{
    public string AccountId { get; set; }
    public string Password { get; set; }
    public string SecurityQuestion { get; set; }
    public string SecurityAnswer { get; set; }

    public EmployeeAccountVerifiedEvent(string accountId, string password, string securityQuestion, string securityAnswer)
    {
        AccountId = accountId;
        Password = password;
        SecurityQuestion = securityQuestion;
        SecurityAnswer = securityAnswer;
    }
}