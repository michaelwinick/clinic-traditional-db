using Events;
using MediatR;

namespace pumper_context.Domain.Commands;

public class UpdateParentCommand : IRequest<bool>
{
    public ParentCreatedEvent Event { get; }

    public UpdateParentCommand(ParentCreatedEvent @event)
    {
        Event = @event;
    }
}