using MediatR;

namespace InboundVerificationGateway.Controllers;

public class MarkVerifiedCommand : IRequest<bool>
{
    public MarkVerifiedCommand(MarkVerifiedDto dto)
    {
        AccountId = dto.AccountId;
        Password = dto.Password;
        SecurityQuestion = dto.SecurityQuestion;
        SecurityAnswer = dto.SecurityAnswer;
    }

    public string SecurityAnswer { get; set; }
    public string SecurityQuestion { get; set; }
    public string Password { get; set; }
    public string AccountId { get; set; }
}