using Events;
using MediatR;

namespace pumper_context.Domain.Commands;

public class UpdateDependentCommand : IRequest<bool>
{
    public DependentCreatedEvent Event { get; }

    public UpdateDependentCommand(DependentCreatedEvent @event)
    {
        Event = @event;
    }
}