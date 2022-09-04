using MediatR;

namespace OutboundVerificationGateway.Domain;


public class SendVerificationEmailCommand : IRequest<bool>
{
    public string AccountId { get; }
    public string Email { get; }

    public SendVerificationEmailCommand(string accountId, string email)
    {
        AccountId = accountId;
        Email = email;
    }
}