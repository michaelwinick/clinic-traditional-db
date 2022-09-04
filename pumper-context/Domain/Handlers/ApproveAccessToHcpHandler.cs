using Dapr.Client;
using Events;
using MediatR;
using pumper_context.Domain.Aggregates;
using pumper_context.Domain.Commands;

namespace pumper_context.Domain.Handlers
{
    public class ApproveAccessToHcpHandler : IRequestHandler<ApproveAccessToHcpCommand, bool>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        private readonly DaprClient _daprClient;


        public ApproveAccessToHcpHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(ApproveAccessToHcpCommand cmd,
            CancellationToken cancellationToken)
        {
            var pumper = await _daprClient.GetStateAsync<Pumper>(DaprStoreName, cmd.PumperAccountId);
            if (pumper == null)
            {
                return false;
            }

            pumper.HcpAssigned.Add(cmd.ClinicId);

            await _daprClient.SaveStateAsync(DaprStoreName, pumper.PumperId, pumper);

            await _daprClient.PublishEventAsync(DaprPubSubName, "HcpAccessApproved",
                new Events.HcpAccessApprovedEvent(cmd.PumperAccountId, cmd.ClinicId));

            return true;
        }
    }
}
