using account_context.Dto;
using MediatR;

namespace account_context.Domain.Commands;

public class CreateDependentAccountCommand : IRequest<string>
{
    public CreateDependentAccountCommand(CreateDependentAccountDto dto)
    {
        ParentId = dto.ParentId;
        DependentFirstName = dto.DependentFirstName;
        Title = dto.Title;
        DependentLastName = dto.DependentLastName;
        Month = dto.Month;
        Day = dto.Day;
        Year = dto.Year;
    }

    public string ParentId { get; set; }
    public string DependentFirstName { get; set; }
    public string Title { get; set; }
    public string DependentLastName { get; set; }
    public string Day { get; set; }
    public string Month { get; set; }
    public string Year { get; set; }
}