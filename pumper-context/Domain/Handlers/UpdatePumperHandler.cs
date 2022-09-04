using Dapr.Client;
using Events;
using MediatR;
using pumper_context.Domain.Aggregates;
using pumper_context.Domain.Commands;

namespace pumper_context.Domain.Handlers
{
    public class UpdatePumperHandler : IRequestHandler<UpdatePumperCommand, bool>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        private readonly DaprClient _daprClient;


        public UpdatePumperHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(UpdatePumperCommand cmd,
            CancellationToken cancellationToken)
        {
            var @event = cmd.Event;

            var pumper = await _daprClient.GetStateAsync<Pumper>(DaprStoreName, @event.AccountId) ?? 
                         new Pumper(@event.AccountId);

            await _daprClient.SaveStateAsync(DaprStoreName, @event.AccountId, pumper);

            await _daprClient.PublishEventAsync(DaprPubSubName, "PumperCreated",
                new PumperCreatedEvent(@event.AccountId));

            Console.WriteLine($"Publishing: PumperCreatedEvent {@event.AccountId}");

            return true;
        }
    }
}
