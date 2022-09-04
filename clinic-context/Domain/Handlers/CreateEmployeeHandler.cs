using clinic_context.Controllers;
using clinic_context.Domain.Aggregates;
using clinic_context.Domain.Commands;
using Dapr.Client;
using Events;
using MediatR;

namespace clinic_context.Domain.Handlers
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, bool>
    {
        private readonly DaprClient _daprClient;
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        public CreateEmployeeHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var clinic = await _daprClient.GetStateAsync<Clinic>(DaprStoreName, command.ClinicId);

            if (clinic == null)
                return false;

            clinic.Employee = command.AccountId;
            clinic.Administrators.Add(command.AccountId);

            await _daprClient.SaveStateAsync(DaprStoreName, command.ClinicId, clinic);

            await _daprClient.PublishEventAsync(DaprPubSubName, nameof(ClinicCreatedEvent),
                new ClinicCreatedEvent(clinic.ClinicId, clinic.AccountId));

            return true;
        }
    }
}
