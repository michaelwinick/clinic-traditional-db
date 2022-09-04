namespace ClinicReadModel.Controllers;

public class Admin
{
    public string AccountId { get; set; }
    public string AuthorizationLevel { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Title { get; set; }

    public Admin()
    {

    }

    public Admin(string accountId, string authorizationLevel, string email, string firstName, string lastName, string title)
    {
        AccountId = accountId;
        AuthorizationLevel = authorizationLevel;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Title = title;
    }
}