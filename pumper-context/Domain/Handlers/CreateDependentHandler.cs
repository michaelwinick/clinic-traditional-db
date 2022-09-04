using Dapr.Client;
using Events;
using MediatR;
using pumper_context.Domain.Aggregates;
using pumper_context.Domain.Commands;

namespace pumper_context.Domain.Handlers
{
    public class CreateDependentHandler : IRequestHandler<CreateDependentCommand, bool>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        private readonly DaprClient _daprClient;


        public CreateDependentHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(CreateDependentCommand cmd,
            CancellationToken cancellationToken)
        {
            await _daprClient.PublishEventAsync(DaprPubSubName, "DependentCreated",
                new DependentCreatedEvent(cmd.AccountId, cmd.ParentId));

            return true;
        }
    }
}
