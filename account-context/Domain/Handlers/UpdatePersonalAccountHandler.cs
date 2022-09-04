using System.Text.Json.Serialization;
using account_context.Controllers;
using account_context.Domain.Aggregates;
using account_context.Domain.Commands;
using Dapr.Client;
using Events;
using MediatR;

namespace account_context.Domain.Handlers
{
    public class UpdatePersonalAccountHandler : IRequestHandler<UpdatePersonalAccountCommand, bool>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";
        private readonly DaprClient _daprClient;


        public UpdatePersonalAccountHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(UpdatePersonalAccountCommand cmd, CancellationToken cancellationToken)
        {
            var @event = cmd.Event;

            var account = await _daprClient.GetStateAsync<Account>(DaprStoreName, @event.AccountId) ?? 
                          new Account(@event.AccountId);

            UpdateAccount(account, @event);

            await _daprClient.SaveStateAsync(DaprStoreName, @event.AccountId, account);

            return true;
        }

        private static void UpdateAccount(Account account, PersonalAccountCreatedEvent @event)
        {
            account.Email = @event.Email;
            account.Password = @event.Password;
            account.SecurityQuestion = @event.SecurityQuestion;
            account.SecurityAnswer = @event.SecurityAnswer;
            account.HealthDataNotice = @event.HealthDataNotice;
            account.TermsOfUse = @event.TermsOfUse;
            account.CurrentState = "Completed";
        }
    }
}
