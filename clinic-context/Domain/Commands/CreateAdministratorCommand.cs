using MediatR;

namespace clinic_context.Domain.Commands;

public class CreateAdministratorCommand : IRequest<bool>
{
    public string ClinicId { get; set; }
    public string AccountId { get; }

    public CreateAdministratorCommand(string accountId, string clinicId)
    {
        ClinicId = clinicId;
        AccountId = accountId;
    }
}