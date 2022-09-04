using Dapr.Client;
using Events;
using MediatR;
using pumper_context.Domain.Aggregates;
using pumper_context.Domain.Commands;

namespace pumper_context.Domain.Handlers
{
    public class PatientAccessRequestedHandler : IRequestHandler<PatientAccessRequestedCommand, bool>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        private readonly DaprClient _daprClient;


        public PatientAccessRequestedHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(PatientAccessRequestedCommand cmd,
            CancellationToken cancellationToken)
        {
            var @event = cmd.Event;

            var pumper = await _daprClient.GetStateAsync<Pumper>(DaprStoreName, @event.PatientId);
            if (pumper == null) return false;

            pumper.HcpRequests.Add(@event.ClinicId);

            await _daprClient.SaveStateAsync(DaprStoreName, pumper.PumperId, pumper);

            await _daprClient.PublishEventAsync(DaprPubSubName, "PumperAccessRequested",
                new PumperAccessRequestedEvent(@event.PatientId, @event.ClinicId));

            return true;
        }
    }
}
