namespace Events;

public class ParentAccountCompletedEvent
{
    public string ParentId { get; set; }
    public string DependentId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string SecurityQuestion { get; set; }
    public string SecurityAnswer { get; set; }
    public string ParentConfirmation { get; set; }
    public string HealthDataNotice { get; set; }
    public string TermsOfUse { get; set; }
    public string CareTakerName { get; set; }

    public ParentAccountCompletedEvent()
    {
        
    }

    //public ParentAccountCompletedEvent(string parentParentId, string dependentId)
    //{
    //    ParentId = parentParentId;
    //    DependentId = dependentId;
    //}

    public ParentAccountCompletedEvent(string parentId, string dependentId, string email, string password, 
        string securityQuestion, string securityAnswer, string parentConfirmation, string healthDataNotice, 
        string termsOfUse, string careTakerName)
    {
        ParentId = parentId;
        DependentId = dependentId;
        Email = email;
        Password = password;
        SecurityQuestion = securityQuestion;
        SecurityAnswer = securityAnswer;
        ParentConfirmation = parentConfirmation;
        HealthDataNotice = healthDataNotice;
        TermsOfUse = termsOfUse;
        CareTakerName = careTakerName;
    }
}