using account_context.Domain.Aggregates;
using account_context.Domain.Commands;
using Dapr.Client;
using Events;
using MediatR;

namespace account_context.Domain.Handlers
{
    public class CreateDependentAccountHandler : IRequestHandler<CreateDependentAccountCommand, string>
    {
        private readonly DaprClient _daprClient;
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        public CreateDependentAccountHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<string> Handle(CreateDependentAccountCommand cmd, CancellationToken cancellationToken)
        {
            var dependentAccountId = Guid.NewGuid().ToString();

            var account = await _daprClient.GetStateAsync<Account>(DaprStoreName, dependentAccountId);

            await _daprClient.PublishEventAsync(DaprPubSubName, "DependentAccountCreated",
                new DependentAccountCreatedEvent(
                    dependentAccountId, 
                    cmd.ParentId,
                    cmd.DependentFirstName, 
                    cmd.Title,
                    cmd.DependentLastName,
                    cmd.Month,
                    cmd.Day,
                    cmd.Year));

            return dependentAccountId;
        }
    }
}
