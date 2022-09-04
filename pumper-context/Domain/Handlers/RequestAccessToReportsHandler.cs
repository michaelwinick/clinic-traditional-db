using Dapr.Client;
using Events;
using MediatR;
using pumper_context.Domain.Aggregates;
using pumper_context.Domain.Commands;

namespace pumper_context.Domain.Handlers
{
    public class RequestAccessToReportsHandler : IRequestHandler<RequestAccessToReportsCommand, bool>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        private readonly DaprClient _daprClient;


        public RequestAccessToReportsHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(RequestAccessToReportsCommand cmd,
            CancellationToken cancellationToken)
        {
            var pumper = await _daprClient.GetStateAsync<Pumper>(DaprStoreName, cmd.PumperAccountId);
            if (pumper == null)
            {
                return false;
            }

            

            await _daprClient.SaveStateAsync(DaprStoreName, pumper.PumperId, pumper);

            await _daprClient.PublishEventAsync(DaprPubSubName, "PatientAccessRequested",
                new PatientAccessRequestedEvent(cmd.ClinicId, cmd.PumperAccountId));

            return true;
        }
    }
}
