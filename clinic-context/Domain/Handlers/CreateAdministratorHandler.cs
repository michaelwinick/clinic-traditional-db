using clinic_context.Domain.Aggregates;
using clinic_context.Domain.Commands;
using Dapr.Client;
using Events;
using MediatR;

namespace clinic_context.Domain.Handlers
{
    public class CreateAdministratorHandler : IRequestHandler<CreateAdministratorCommand, bool>
    {
        private readonly DaprClient _daprClient;
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        public CreateAdministratorHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(CreateAdministratorCommand command, CancellationToken cancellationToken)
        {
            var clinic = await _daprClient.GetStateAsync<Clinic>(DaprStoreName, command.ClinicId);

            if (clinic == null)
            {
                return false;
            }

            clinic = new Clinic(command.ClinicId, command.AccountId);
            clinic.Administrator = command.AccountId;
            //clinic.Administrators.Add(command.PumperId);

            await _daprClient.SaveStateAsync(DaprStoreName, command.ClinicId, clinic);

            await _daprClient.PublishEventAsync(DaprPubSubName, nameof(ClinicCreatedEvent),
                new ClinicCreatedEvent(clinic.ClinicId, clinic.AccountId));

            return true;
        }
    }
}
