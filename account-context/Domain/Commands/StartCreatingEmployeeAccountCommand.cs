using account_context.Dto;
using MediatR;

namespace account_context.Domain.Commands;

public class StartCreatingEmployeeAccountCommand : IRequest<string>
{
    public StartCreatingEmployeeAccountCommand(StartCreatingEmployeeAccountDto dto)
    {
        ClinicId = dto.ClinicId;
        FirstName = dto.FirstName;
        LastName = dto.LastName;
        Title = dto.Title;
        Email = dto.Email;
    }

    public string ClinicId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Title { get; set; }
    public string Email { get; set; }
}   