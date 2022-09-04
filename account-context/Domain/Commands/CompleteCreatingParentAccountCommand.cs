using account_context.Dto;
using MediatR;

namespace account_context.Domain.Commands;

public class CompleteCreatingParentAccountCommand : IRequest<bool>
{
    public CompleteCreatingParentAccountCommand(CompleteCreatingParentAccountDto dto)
    {
        ParentId = dto.ParentId;
        DependentId = dto.DependentId;
        Email = dto.Email;
        Password = dto.Password;
        SecurityQuestion = dto.SecurityQuestion;
        SecurityAnswer = dto.SecurityAnswer;
        ParentConfirmation = dto.ParentConfirmation;
        HealthDataNotice = dto.HealthDataConfirmation;
        TermsOfUse = dto.TermsOfService;
    }

    public string ParentId { get; set; }
    public string DependentId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string SecurityAnswer { get; set; }
    public string SecurityQuestion { get; set; }
    public string ParentConfirmation { get; set; }
    public string HealthDataNotice { get; set; }
    public string TermsOfUse { get; set; }
}