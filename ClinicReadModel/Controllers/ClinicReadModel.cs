namespace ClinicReadModel.Controllers;

public class ClinicReadModel
{
    public string ClinicId { get; set; }
    public string CompanyName { get; set; }
    public string Address { get; set; }
    public string Country { get; set; }
    public HashSet<Admin> ProfessionalAccounts { get; set; } = new();
    public HashSet<Patient> Patients { get; set; } = new();


    public ClinicReadModel()
    {
        
    }

    public ClinicReadModel(string clinicId)
    {
        ClinicId = clinicId;
    }
}