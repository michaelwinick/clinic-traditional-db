using account_context.Dto;
using MediatR;

namespace account_context.Domain.Commands
{

public class AddPersonalAccountInformationCommand : IRequest<bool>
{
    public AddPersonalAccountInformationCommand(AddPersonalAccountInformationDto addDto)
    {
        AccountId = addDto.AccountId;
        FirstName = addDto.FirstName;
        LastName = addDto.LastName;
        Dob = addDto.DoB;
    }

    public string Dob { get; }
    public string LastName { get; }
    public string FirstName { get; }
    public string AccountId { get; set; }
}
}