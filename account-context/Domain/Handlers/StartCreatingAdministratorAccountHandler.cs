using System.Text.Json.Serialization;
using account_context.Controllers;
using account_context.Domain.Aggregates;
using account_context.Domain.Commands;
using Dapr.Client;
using Events;
using MediatR;

namespace account_context.Domain.Handlers
{
    public class StartCreatingAdministratorAccountHandler : IRequestHandler<StartCreatingAdministratorAccountCommand, string>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        private readonly DaprClient _daprClient;


        public StartCreatingAdministratorAccountHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<string> Handle(StartCreatingAdministratorAccountCommand cmd,
            CancellationToken cancellationToken)
        {
            var newAccountId = Guid.NewGuid().ToString();

            var account = UpdateAccount(cmd, newAccountId);

            await _daprClient.SaveStateAsync(DaprStoreName, newAccountId, account);

            await _daprClient.PublishEventAsync(DaprPubSubName, "AdministratorAccountCreationStarted",
                new AdministratorAccountCreationStartedEvent(newAccountId));

            return newAccountId;
        }

        private static Account UpdateAccount(StartCreatingAdministratorAccountCommand request, string accountId)
        {
            return new Account(accountId)
            {
                CurrentState = "Started",
                AccountType = "Administrator",
                ClinicId = request.ClinicId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Title = request.Title,
                DefaultLanguage = request.DefaultLanguage
            };
        }
    }
}
