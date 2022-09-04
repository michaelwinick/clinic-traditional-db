using Dapr.Client;
using Events;
using MediatR;
using pumper_context.Domain.Aggregates;
using pumper_context.Domain.Commands;

namespace pumper_context.Domain.Handlers
{
    public class ApproveHcpAccessHandler : IRequestHandler<ApproveHcpAccessCommand, bool>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        private readonly DaprClient _daprClient;


        public ApproveHcpAccessHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(ApproveHcpAccessCommand cmd,
            CancellationToken cancellationToken)
        {
            var pumper = await _daprClient.GetStateAsync<Pumper>(DaprStoreName, cmd.PatientId);
            if (pumper == null)
            {
                return false;
            }

            pumper.HcpAssigned = cmd.HcpId;

            await _daprClient.SaveStateAsync(DaprStoreName, pumper.PumperId, pumper);

            await _daprClient.PublishEventAsync(DaprPubSubName, "PumperAccessRequested",
                new PumperAccessRequestedEvent(cmd.PatientId, cmd.PatientId));

            return true;
        }
    }
}
