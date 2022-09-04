namespace Events;

public class ClinicAccountCreatedEvent 
{
    public string AccountId { get; set; }
    public string ClinicId { get; set; }
    public string CompanyName { get; set; }
    public string Address { get; set; }
    public string Country { get; set; }

    public ClinicAccountCreatedEvent()
    {
        
    }

    public ClinicAccountCreatedEvent(string accountId, string clinicId, string companyName, string address, string country)
    {
        AccountId = accountId;
        ClinicId = clinicId;
        CompanyName = companyName;
        Address = address;
        Country = country;
    }
}