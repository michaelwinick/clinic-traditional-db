using Dapr.Client;
using Events;
using MediatR;
using pumper_context.Domain.Aggregates;
using pumper_context.Domain.Commands;

namespace pumper_context.Domain.Handlers
{
    public class CreatePumperHandler : IRequestHandler<CreatePumperCommand, bool>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        private readonly DaprClient _daprClient;


        public CreatePumperHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(CreatePumperCommand cmd,
            CancellationToken cancellationToken)
        {
            await _daprClient.PublishEventAsync(DaprPubSubName, "PumperCreated",
                new PumperCreatedEvent(cmd.AccountId));

            Console.WriteLine($"Published Event: PumperCreatedEvent {cmd.AccountId}");

            return true;
        }
    }
}
