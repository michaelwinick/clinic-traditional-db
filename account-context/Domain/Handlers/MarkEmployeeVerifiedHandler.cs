using System.Text.Json.Serialization;
using account_context.Controllers;
using account_context.Domain.Aggregates;
using account_context.Domain.Commands;
using Dapr.Client;
using Events;
using MediatR;

namespace account_context.Domain.Handlers
{
    public class MarkEmployeeVerifiedHandler : IRequestHandler<MarkEmployeeVerifiedCommand, bool>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        private readonly DaprClient _daprClient;


        public MarkEmployeeVerifiedHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(MarkEmployeeVerifiedCommand cmd,
            CancellationToken cancellationToken)
        {
            var account = await _daprClient.GetStateAsync<Account>(DaprStoreName, cmd.AccountId);
            if (account == null) return false;

            account.IsVerified = true;
            await _daprClient.SaveStateAsync(DaprStoreName, account.AccountId, account);

            await _daprClient.PublishEventAsync(DaprPubSubName, "EmployeeAccountCreated",
                new EmployeeAccountCreatedEvent(
                    cmd.AccountId,
                    account.ClinicId,
                    account.Email,
                    account.FirstName,
                    account.LastName,
                    account.Title));

            return true;
        }
    }
}
