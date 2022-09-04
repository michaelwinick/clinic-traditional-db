using System.Xml;
using account_context.Dto;
using MediatR;

namespace account_context.Domain.Commands
{

    public class AddParentAccountInformationCommand : IRequest<bool>
    {
        public AddParentAccountInformationCommand(AddParentAccountInformationDto dto)
        {
            ParentId = dto.ParentId;
            FirstName = dto.ParentFirstName;
            LastName = dto.ParentLastName;
            Month = dto.Month;
            Day = dto.Day;
            Year = dto.Year;
            Country = dto.Country;
            Region = dto.Region;
        }

        public string ParentId { set; get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string Year { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
    }
}