using account_context.Dto;
using MediatR;

namespace account_context.Domain.Commands;

public class CompleteCreatingClinicAccountCommand : IRequest<bool>
{
    public CompleteCreatingClinicAccountCommand(CompleteCreatingClinicAccountDto accountDto)
    {
        AccountId = AccountId = accountDto.AccountId;
        Name = accountDto.ClinicName;
        Address = accountDto.ClinicAddress;
        Country = accountDto.Country;
        Region = accountDto.Region;
    }

    public string AccountId { get; set; }
    public string ClinicId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Country { get; set; }
    public string Region { get; set; }
}