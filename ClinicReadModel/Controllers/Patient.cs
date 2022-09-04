namespace ClinicReadModel.Controllers;

public class Patient : IEquatable<Patient>
{
    public string AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Dob { get; set; }
    public string CareTakerName { get; set; }

    public Patient()
    {
        
    }

    public Patient(string accountId, string firstName, string lastName, string email, string dob,
        string careTakerName) 
    {
        AccountId = accountId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Dob = dob;
        CareTakerName = careTakerName;
    }

    public override bool Equals(Object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Patient)obj);
    }

    public bool Equals(Patient? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return AccountId == other.AccountId && FirstName == other.FirstName && LastName == other.LastName && Email == other.Email && Dob == other.Dob && CareTakerName == other.CareTakerName;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(AccountId, FirstName, LastName, Email, Dob, CareTakerName);
    }
}