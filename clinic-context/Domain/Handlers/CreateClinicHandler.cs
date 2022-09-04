using clinic_context.Domain.Aggregates;
using clinic_context.Domain.Commands;
using Dapr.Client;
using Events;
using MediatR;

namespace clinic_context.Domain.Handlers
{
    public class CreateClinicHandler : IRequestHandler<CreateClinicCommand, bool>
    {
        private readonly DaprClient _daprClient;
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        public CreateClinicHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(CreateClinicCommand command, CancellationToken cancellationToken)
        {
            var clinic = await _daprClient.GetStateAsync<Clinic>(DaprStoreName, command.ClinicId);

            if (clinic != null)
            {
                return false;
                //throw new Exception($"Clinic: {command.hcpId} already exists.");
            }

            clinic = new Clinic(command.ClinicId, command.AccountId);

            await _daprClient.SaveStateAsync(DaprStoreName, command.ClinicId, clinic);

            await _daprClient.PublishEventAsync(DaprPubSubName, nameof(ClinicCreatedEvent),
                new ClinicCreatedEvent(clinic.ClinicId, clinic.AccountId));

            return true;
        }
    }
}
