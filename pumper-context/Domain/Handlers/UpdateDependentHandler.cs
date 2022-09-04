using Dapr.Client;
using Events;
using MediatR;
using pumper_context.Domain.Aggregates;
using pumper_context.Domain.Commands;

namespace pumper_context.Domain.Handlers
{
    public class UpdateDependentHandler : IRequestHandler<UpdateDependentCommand, bool>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        private readonly DaprClient _daprClient;


        public UpdateDependentHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(UpdateDependentCommand cmd,
            CancellationToken cancellationToken)
        {
            var @event = cmd.Event;

            var pumper = await _daprClient.GetStateAsync<Pumper>(DaprStoreName, @event.AccountId) ?? 
                         new Pumper(@event.AccountId, @event.ParentId);

            await _daprClient.SaveStateAsync(DaprStoreName, @event.AccountId, pumper);

            return true;
        }
    }
}
