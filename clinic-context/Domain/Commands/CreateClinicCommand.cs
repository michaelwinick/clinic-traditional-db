using MediatR;

namespace clinic_context.Domain.Commands;

public class CreateClinicCommand : IRequest<bool>
{
    public string ClinicId { get; set; }
    public string AccountId { get; }

    public CreateClinicCommand(string clinicId, string accountId)
    {
        ClinicId = clinicId;
        AccountId = accountId;
    }
}