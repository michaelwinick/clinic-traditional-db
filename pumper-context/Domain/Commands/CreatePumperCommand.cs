using MediatR;

namespace pumper_context.Domain.Commands;

public class CreatePumperCommand : IRequest<bool>
{
    public string AccountId { get; }

    public CreatePumperCommand(string accountId)
    {
        AccountId = accountId;
    }
}