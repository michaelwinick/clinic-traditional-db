using System.Text.Json.Serialization;
using account_context.Controllers;
using account_context.Domain.Aggregates;
using account_context.Domain.Commands;
using Dapr.Client;
using Events;
using MediatR;

namespace account_context.Domain.Handlers
{
    public class AddParentAccountInformationHandler : IRequestHandler<AddParentAccountInformationCommand, bool>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";
        private readonly DaprClient _daprClient;


        public AddParentAccountInformationHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(AddParentAccountInformationCommand cmd, CancellationToken cancellationToken)
        {
            var accountId = cmd.ParentId;

            var account = await _daprClient.GetStateAsync<Account>(DaprStoreName, accountId);
            if (account == null) throw new Exception($"Account: {accountId} not found.");

            if (account.CurrentState != "Started")
                throw new Exception($"Account: {accountId} is in state: {account.CurrentState}.");

            UpdateAccount(cmd, account);
            account.CurrentState = "InformationAdded";

            await _daprClient.SaveStateAsync(DaprStoreName, accountId, account);

            await _daprClient.PublishEventAsync(DaprPubSubName, "ParentAccountInformationAdded",
                new ParentAccountInformationAddedEvent(cmd.ParentId));

            return true;
        }

        private static void UpdateAccount(AddParentAccountInformationCommand cmd, Account account)
        {
            account.FirstName = cmd.FirstName;
            account.LastName = cmd.LastName;
            account.Month = cmd.Month;
            account.Day = cmd.Day;
            account.Year = cmd.Year;
            account.Country = cmd.Country;
            account.Region = cmd.Region;
        }
    }
}
