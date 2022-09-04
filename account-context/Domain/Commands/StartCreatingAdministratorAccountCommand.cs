using account_context.Dto;
using MediatR;

namespace account_context.Domain.Commands;

public class StartCreatingAdministratorAccountCommand : IRequest<string>
{
    public StartCreatingAdministratorAccountCommand(StartCreatingAdministratorAccountDto dto)
    {
        ClinicId = dto.ClinicId;
        FirstName = dto.FirstName;
        LastName = dto.LastName;
        Title = dto.Title;
        DefaultLanguage = dto.DefaultLanguage;
    }

    public string ClinicId { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Title { get; }
    public string DefaultLanguage { get; }
}   