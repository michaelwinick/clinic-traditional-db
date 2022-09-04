using account_context.Dto;
using MediatR;

namespace account_context.Domain.Commands;

public class StartCreatingParentAccountCommand : IRequest<string>
{
    public string Invitation { get; }

    public StartCreatingParentAccountCommand(StartCreatingParentAccountDto dto)
    {
        Invitation = dto.Invitation;
    }
}