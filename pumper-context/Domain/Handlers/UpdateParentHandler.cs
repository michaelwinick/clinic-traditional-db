using Dapr.Client;
using Events;
using MediatR;
using pumper_context.Domain.Aggregates;
using pumper_context.Domain.Commands;

namespace pumper_context.Domain.Handlers
{
    public class UpdateParentHandler : IRequestHandler<UpdateParentCommand, bool>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        private readonly DaprClient _daprClient;


        public UpdateParentHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(UpdateParentCommand cmd,
            CancellationToken cancellationToken)
        {
            var @event = cmd.Event;

            var pumper = await _daprClient.GetStateAsync<Pumper>(DaprStoreName, @event.AccountId);
            if (pumper != null) return false;

            await _daprClient.SaveStateAsync(DaprStoreName, @event.AccountId, new Pumper(@event.AccountId));

            return true;
        }
    }
}
