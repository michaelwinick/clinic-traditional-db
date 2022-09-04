namespace AccountsReadModel.Controllers;

public class AccountsReadModel
{
    public string AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Dob { get; set; }
    public string CareTaker { get; set; }
    public string CareTakerName { get; set; }

    public AccountsReadModel()
    {
        
    }

    public AccountsReadModel(string accountId)
    {
        AccountId = accountId;
    }

} 