using System.Text.Json;
using System.Text.Json.Serialization;
using account_context.Controllers;
using account_context.Domain.Aggregates;
using account_context.Domain.Commands;
using Dapr.Client;
using Events;
using MediatR;

namespace account_context.Domain.Handlers
{
    public class StartCreatingPersonalAccountHandler : 
        IRequestHandler<StartCreatingPersonalAccountCommand, string>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        private readonly DaprClient _daprClient;


        public StartCreatingPersonalAccountHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<string> Handle(StartCreatingPersonalAccountCommand cmd,
            CancellationToken cancellationToken)
        {
            //var result = _daprClient.CreateInvokeMethodRequest(
            //    HttpMethod.Get, "clinic-invitation-ready-model", "api/ClinicInvitation/IsInvitionValid/qqq");
            //await _daprClient.InvokeMethodAsync(result);
            //var json = result.ToString();
            //var deserialize = JsonSerializer.Deserialize<bool>(json);


            var newAccountId = Guid.NewGuid().ToString();
            
            var account = new Account(newAccountId, cmd.Invitation);
            account.AccountType = "Pumper";
            account.CurrentState = "Started";

            await _daprClient.SaveStateAsync(DaprStoreName, newAccountId,
                account);

            await _daprClient.PublishEventAsync(DaprPubSubName, "PersonalAccountCreationStarted",
                new PersonalAccountCreationStartedEvent(newAccountId, cmd.Invitation));

            return newAccountId;
        }
    }
}
