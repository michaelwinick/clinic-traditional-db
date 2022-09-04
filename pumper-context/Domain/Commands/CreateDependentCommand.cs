using MediatR;

namespace pumper_context.Domain.Commands;

public class CreateDependentCommand : IRequest<bool>
{
    public string AccountId { get; }
    public string ParentId { get; }

    public CreateDependentCommand(string accountId, string parentId)
    {
        AccountId = accountId;
        ParentId = parentId;
    }
}