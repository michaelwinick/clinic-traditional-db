using Events;
using MediatR;

namespace account_context.Controllers;

public class UpdateParentAccountCommand : IRequest<bool>
{
    public ParentAccountCompletedEvent Event { get; }

    public UpdateParentAccountCommand(ParentAccountCompletedEvent @event)
    {
        Event = @event;
    }
}