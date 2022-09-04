using Dapr.Client;
using Events;
using MediatR;
using pumper_context.Domain.Aggregates;
using pumper_context.Domain.Commands;

namespace pumper_context.Domain.Handlers
{
    public class GiveAccessToHcpHandler : IRequestHandler<GiveAccessToHcpCommand, bool>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        private readonly DaprClient _daprClient;


        public GiveAccessToHcpHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(GiveAccessToHcpCommand cmd,
            CancellationToken cancellationToken)
        {
            var pumper = await _daprClient.GetStateAsync<Pumper>(DaprStoreName, cmd.PumperAccountId);
            if (pumper == null)
            {
                pumper = new Pumper(cmd.PumperAccountId);
            }

            pumper.HcpAssigned.Add(cmd.ClinicId);

            await _daprClient.SaveStateAsync(DaprStoreName, pumper.PumperId, pumper);

            await _daprClient.PublishEventAsync(DaprPubSubName, "HcpAccessApproved",
                new HcpAccessApprovedEvent(cmd.PumperAccountId, cmd.ClinicId));

            return true;
        }
    }
}
