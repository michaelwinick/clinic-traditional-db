using Events;
using MediatR;

namespace account_context.Domain.Commands;

public class MarkEmployeeVerifiedCommand : IRequest<bool>
{
    public MarkEmployeeVerifiedCommand(EmployeeAccountVerifiedEvent @event)
    {
        AccountId = @event.AccountId;
        Password = @event.Password;
        SecurityQuestion = @event.SecurityQuestion;
        SecurityAnswer = @event.SecurityAnswer;
    }

    public string SecurityAnswer { get; set; }
    public string SecurityQuestion { get; set; }
    public string Password { get; set; }
    public string AccountId { get; set; }
}