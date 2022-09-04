using Events;
using MediatR;

namespace pumper_context.Domain.Commands;

public class PatientAccessRequestedCommand : IRequest<bool>
{
    public PatientAccessRequestedEvent Event { get; }

    public PatientAccessRequestedCommand(PatientAccessRequestedEvent @event)
    {
        Event = @event;
    }
}