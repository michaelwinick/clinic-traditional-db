using MediatR;

namespace clinic_context.Controllers;

public class CreateEmployeeCommand : IRequest<bool>
{
    public string AccountId { get; set; }
    public string ClinicId { get; set; }

    public CreateEmployeeCommand(string accountId, string clinicId)
    {
        AccountId = accountId;
        ClinicId = clinicId;
    }
}