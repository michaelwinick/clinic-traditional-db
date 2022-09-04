using account_context.Dto;
using MediatR;

namespace account_context.Domain.Commands;

public class CompletePersonalAccountCommand : IRequest<bool>
{
    public CompletePersonalAccountCommand(CompletePersonalAccountDto completeDto)
    {
        AccountId = completeDto.AccountId;
        Email = completeDto.Email;
        Password = completeDto.Password;
        SecurityQuestion = completeDto.SecurityQuestion;
        SecurityAnswer = completeDto.SecurityAnswer;
        HealthDataNotice = completeDto.HealthDataNotice;
        TermsOfUse = completeDto.TermsOfUse;
    }

    public string TermsOfUse { get; set; }
    public string HealthDataNotice { get; set; }
    public string SecurityAnswer { get; set; }
    public string SecurityQuestion { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string AccountId { get; set; }
}