using Events;
using MediatR;

namespace account_context.Domain.Commands;

public class UpdateDependentAccountCommand : IRequest<bool>
{
    public DependentAccountCreatedEvent Event { get; }

    public UpdateDependentAccountCommand(DependentAccountCreatedEvent @event)
    {
        Event = @event;
    }
}