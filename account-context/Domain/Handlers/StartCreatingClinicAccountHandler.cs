using account_context.Domain.Aggregates;
using account_context.Domain.Commands;
using Dapr.Client;
using Events;
using MediatR;

namespace account_context.Domain.Handlers
{
    public class StartCreatingClinicAccountHandler : IRequestHandler<StartCreatingClinicAccountCommand, string>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";
        private readonly DaprClient _daprClient;

        public StartCreatingClinicAccountHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<string> Handle(StartCreatingClinicAccountCommand cmd, CancellationToken cancellationToken)
        {
            var newAccountId = Guid.NewGuid().ToString();

            var account = new Account(newAccountId);
            account.ClinicId = cmd.ClinicId;
            account.CurrentState = "Started";
            account.AccountType = "CLINIC";

            try
            {
                await _daprClient.SaveStateAsync(DaprStoreName, newAccountId, account);

                await _daprClient.PublishEventAsync(DaprPubSubName, "ClinicCreationStarted",
                    new ClinicCreationStartedEvent(cmd.ClinicId, newAccountId));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return newAccountId;
        }
    }
}
