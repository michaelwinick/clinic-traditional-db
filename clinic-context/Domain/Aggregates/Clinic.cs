using System.Text.Json.Serialization;

namespace clinic_context.Domain.Aggregates;

public class Clinic
{
    public Clinic(string clinicId, string accountId)
    {
        ClinicId = clinicId;
        AccountId = accountId;
        Administrators = new List<string>();
    }

    [JsonPropertyName("clinicId")]
    public string ClinicId { get; set; }

    [JsonPropertyName("accountId")]
    public string AccountId { get; set; }

    [JsonPropertyName("administrator")]
    public string Administrator { get; set; }

    [JsonPropertyName("employee")]
    public string Employee { get; set; }

    [JsonPropertyName("approvedPatient")] 
    public HashSet<string> ApprovedPatients { get; set; } = new();

    [JsonPropertyName("outstandingAccessRequests")]
    public HashSet<string> OutstandingAccessRequests { get; set; } = new();

    [JsonPropertyName("administrators")] 
    public List<string> Administrators;
}