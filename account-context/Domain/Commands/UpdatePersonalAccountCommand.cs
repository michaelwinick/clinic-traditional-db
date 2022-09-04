using Events;
using MediatR;

namespace account_context.Domain.Commands;

public class UpdatePersonalAccountCommand : IRequest<bool>
{
    public PersonalAccountCreatedEvent Event { get; }

    public UpdatePersonalAccountCommand(PersonalAccountCreatedEvent @event)
    {
        Event = @event;
    }
}