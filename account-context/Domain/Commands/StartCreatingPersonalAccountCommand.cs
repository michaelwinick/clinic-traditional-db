using account_context.Dto;
using MediatR;

namespace account_context.Domain.Commands;

public class StartCreatingPersonalAccountCommand : IRequest<string>
{
    public string Invitation { get; }

    public StartCreatingPersonalAccountCommand(StartCreatingPersonalAccountDto dto)
    {
        Invitation = dto.Invitation;
    }
}