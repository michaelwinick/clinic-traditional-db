using System.Text.Json.Serialization;
using account_context.Controllers;
using account_context.Domain.Aggregates;
using account_context.Domain.Commands;
using Dapr.Client;
using Events;
using MediatR;

namespace account_context.Domain.Handlers
{
    public class AddPersonalAccountInformationHandler : IRequestHandler<AddPersonalAccountInformationCommand, bool>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";
        private readonly DaprClient _daprClient;


        public AddPersonalAccountInformationHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(AddPersonalAccountInformationCommand cmd, CancellationToken cancellationToken)
        {
            var accountId = cmd.AccountId;
            
            var account = await _daprClient.GetStateAsync<Account>(DaprStoreName, accountId);
            if (account == null) throw new Exception($"Account: {accountId} not found.");

            if (account.CurrentState != "Started") 
                throw new Exception($"Account: {accountId} is in state: {account.CurrentState}.");

            UpdateAccount(cmd, account);
            account.CurrentState = "InformationAdded";

            await _daprClient.SaveStateAsync(DaprStoreName, accountId, account);

            await _daprClient.PublishEventAsync(DaprPubSubName, "PersonalAccountInformationAddedEvent",
                new PersonalAccountInformationAddedEvent(
                    account.AccountId, account.FirstName, account.LastName, account.Dob));

            return true;
        }

        private static void UpdateAccount(AddPersonalAccountInformationCommand cmd, Account account)
        {
            account.FirstName = cmd.FirstName;
            account.LastName = cmd.LastName;
            account.Dob = cmd.Dob;
        }
    }
}
