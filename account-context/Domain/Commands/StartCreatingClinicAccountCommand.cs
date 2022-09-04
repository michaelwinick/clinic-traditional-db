using account_context.Dto;
using MediatR;

namespace account_context.Domain.Commands;

public class StartCreatingClinicAccountCommand : IRequest<string>
{
    public string ClinicId { get; set; }

    public StartCreatingClinicAccountCommand(StartCreatingClinicAccountDto startDto)
    {
        ClinicId = startDto.ClinicId;
    }
}