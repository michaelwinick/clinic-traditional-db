using Events;
using MediatR;

namespace pumper_context.Domain.Commands;

public class CreateParentCommand : IRequest<bool>
{
    public string AccountId { get; }
    public string DependentId { get; }


    public CreateParentCommand(ParentAccountCompletedEvent @event)
    {
        AccountId = @event.ParentId;
        DependentId = @event.DependentId;
    }

    public CreateParentCommand(string accountId, string dependentId)
    {
        AccountId = accountId;
        DependentId = dependentId;
    }
}