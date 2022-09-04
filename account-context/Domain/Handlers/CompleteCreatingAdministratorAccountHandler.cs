using System.Text.Json.Serialization;
using account_context.Controllers;
using account_context.Domain.Aggregates;
using account_context.Domain.Commands;
using Dapr.Client;
using Events;
using MediatR;

namespace account_context.Domain.Handlers
{
    public class CompleteCreatingAdministratorAccountHandler : IRequestHandler<CompleteCreatingAdministratorAccountCommand, bool>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        private readonly DaprClient _daprClient;


        public CompleteCreatingAdministratorAccountHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(CompleteCreatingAdministratorAccountCommand cmd,
            CancellationToken cancellationToken)
        {
            var adminAccount = await _daprClient.GetStateAsync<Account>(DaprStoreName, cmd.AccountId);

            if (adminAccount == null) throw new Exception($"Account: {cmd.AccountId} not found.");

            if (cmd.ClinicId != adminAccount.ClinicId)
                throw new Exception($"Account: {cmd.AccountId} doesn't belong to clinic {cmd.ClinicId}");

            if (adminAccount.CurrentState != "Started")
                throw new Exception($"Account: {cmd.AccountId} is in state: {adminAccount.CurrentState}.");

            UpdateAccount(adminAccount, cmd);
            adminAccount.CurrentState = "Completed";

            await _daprClient.SaveStateAsync(DaprStoreName, cmd.AccountId, adminAccount);

            await _daprClient.PublishEventAsync(DaprPubSubName, "AdministratorAccountCreated",
                new AdministratorAccountCreatedEvent(
                    cmd.AccountId, 
                    cmd.ClinicId, 
                    cmd.Email, 
                    adminAccount.FirstName,
                    adminAccount.LastName,
                    adminAccount.Title));

            return true;
        }

        private static void UpdateAccount(Account account, CompleteCreatingAdministratorAccountCommand command)
        {
            account.Email = command.Email;
            account.SecurityQuestion = command.SecurityQuestion;
            account.SecurityAnswer = command.SecurityAnswer;
            account.Consent = command.Consent;
        }
    }
}
