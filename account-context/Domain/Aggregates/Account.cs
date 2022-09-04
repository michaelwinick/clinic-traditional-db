using System.Text.Json.Serialization;

namespace account_context.Domain.Aggregates
{
    public class Account
    {
        public Account()
        {
            
        }

        public Account(string accountId,  string invitationId)
        {
            this.AccountId = accountId;
            this.InvitationId = invitationId;
        }

        public Account(string accountId)
        {
            this.AccountId = accountId;
        }

        [JsonPropertyName("accountId")]
        public string AccountId { get; init; }

        [property: JsonPropertyName("clinicId")]
        public string ClinicId { get; set; }

        [property: JsonPropertyName("accountType")]
        public string AccountType { get; set; }

        [property: JsonPropertyName("currentState")]
        public string CurrentState { get; set; } = "Started";

        [JsonPropertyName("invitationId")]
        public string InvitationId { get; init; }

        [property: JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [property: JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [property: JsonPropertyName("dob")]
        public string Dob { get; set; }

        [property: JsonPropertyName("email")]
        public string? Email { get; set; }

        [property: JsonPropertyName("password")]
        public string? Password { get; set; }

        [property: JsonPropertyName("securityQuestion")]
        public string? SecurityQuestion { get; set; }

        [property: JsonPropertyName("securityAnswer")]
        public string? SecurityAnswer { get; set; }

        [property: JsonPropertyName("healthDataNotice")]
        public string? HealthDataNotice { get; set; }

        [property: JsonPropertyName("termsOfUse")]
        public string? TermsOfUse { get; set; }

        [property: JsonPropertyName("title")]
        public string? Title { get; set; }

        [property: JsonPropertyName("defaultLanguage")]
        public string? DefaultLanguage { get; set; }

        [property: JsonPropertyName("companyName")]
        public string? CompanyName { get; set; }

        [property: JsonPropertyName("address")]
        public string? Address { get; set; }

        [property: JsonPropertyName("country")]
        public string? Country { get; set; }

        [property: JsonPropertyName("consent")]
        public string? Consent { get; set; }

        [property: JsonPropertyName("isVerified")]
        public bool IsVerified { get; set; }

        [property: JsonPropertyName("Region")]
        public string? Region { get; set; }
        
        [property: JsonPropertyName("Month")]
        public string Month { get; set; }

        [property: JsonPropertyName("Dat")]
        public string Day { get; set; }

        [property: JsonPropertyName("Year")]
        public string Year { get; set; }


        [property: JsonPropertyName("parentId")]
        public string ParentId { get; set; }

        [property: JsonPropertyName("parentConfirmation")]
        public string ParentConfirmation { get; set; }

        [property: JsonPropertyName("dependentId")]
        public string DependentId { get; set; }
    }
}
