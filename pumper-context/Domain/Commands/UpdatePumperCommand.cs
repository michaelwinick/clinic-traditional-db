using Events;
using MediatR;

namespace pumper_context.Domain.Commands;

public class UpdatePumperCommand : IRequest<bool>
{
    public PersonalAccountCreatedEvent Event { get; }

    public UpdatePumperCommand(PersonalAccountCreatedEvent @event)
    {
        Event = @event;
    }
}