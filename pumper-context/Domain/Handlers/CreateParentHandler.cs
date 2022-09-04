using Dapr.Client;
using Events;
using MediatR;
using pumper_context.Domain.Aggregates;
using pumper_context.Domain.Commands;

namespace pumper_context.Domain.Handlers
{
    public class CreateParentHandler : IRequestHandler<CreateParentCommand, bool>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        private readonly DaprClient _daprClient;


        public CreateParentHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(CreateParentCommand cmd,
            CancellationToken cancellationToken)
        {
            await _daprClient.PublishEventAsync(DaprPubSubName, "PumperCreated",
                new PumperCreatedEvent(cmd.DependentId, cmd.AccountId));

            return true;
        }
    }
}
