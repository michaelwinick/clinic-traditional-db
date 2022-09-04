using clinic_context.Controllers;
using clinic_context.Domain.Aggregates;
using clinic_context.Domain.Commands;
using Dapr.Client;
using Events;
using MediatR;

namespace clinic_context.Domain.Handlers
{
    public class RequestAccessToReportsHandler : IRequestHandler<RequestAccessToReportsCommand, bool>
    {
        private readonly DaprClient _daprClient;
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        public RequestAccessToReportsHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(RequestAccessToReportsCommand command, CancellationToken cancellationToken)
        {
            var clinic = await _daprClient.GetStateAsync<Clinic>(DaprStoreName, command.ClinicId);

            if (clinic == null) return false;

            clinic.OutstandingAccessRequests.Add(command.PumperAccountId);

            await _daprClient.SaveStateAsync(DaprStoreName, command.ClinicId, clinic);

            await _daprClient.PublishEventAsync(DaprPubSubName, "PatientAccessRequested",
                new PatientAccessRequestedEvent(clinic.ClinicId, clinic.AccountId));

            return true;
        }
    }
}
