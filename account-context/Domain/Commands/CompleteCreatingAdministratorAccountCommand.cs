using System.Security.Permissions;
using account_context.Dto;
using MediatR;

namespace account_context.Domain.Commands;

public class CompleteCreatingAdministratorAccountCommand : IRequest<bool>
{
    public CompleteCreatingAdministratorAccountCommand(CompleteCreatingAdministratorAccountDto dto)
    {
        AccountId = dto.AccountId;
        ClinicId = dto.ClinicId;
        Email = dto.Email;
        Password = dto.Password;
        SecurityQuestion = dto.SecurityQuestion;
        SecurityAnswer = dto.SecurityAnswer;
        Consent = dto.Consent;
    }

    public string Consent { get; set; }
    public string SecurityAnswer { get; set; }
    public string SecurityQuestion { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string AccountId { get; set; }
    public string ClinicId { get; }
}   