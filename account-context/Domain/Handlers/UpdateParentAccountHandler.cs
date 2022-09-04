using System.Text.Json.Serialization;
using account_context.Controllers;
using account_context.Domain.Aggregates;
using account_context.Domain.Commands;
using Dapr.Client;
using Events;
using MediatR;

namespace account_context.Domain.Handlers
{
    public class UpdateParentAccountHandler : IRequestHandler<UpdateParentAccountCommand, bool>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";
        private readonly DaprClient _daprClient;


        public UpdateParentAccountHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(UpdateParentAccountCommand cmd, CancellationToken cancellationToken)
        {
            var @event = cmd.Event;

            var account = await _daprClient.GetStateAsync<Account>(DaprStoreName, @event.ParentId) ?? 
                          new Account(@event.ParentId);

            UpdateClinic(@event, account);

            await _daprClient.SaveStateAsync(DaprStoreName, @event.ParentId, account);

            return true;
        }

        private void UpdateClinic(ParentAccountCompletedEvent cmd, Account account)
        {
            account.DependentId = cmd.DependentId;
            account.Email = cmd.Email;
            account.Password = cmd.Password;
            account.SecurityQuestion = cmd.SecurityQuestion;
            account.SecurityAnswer = cmd.SecurityAnswer;
            account.ParentConfirmation = cmd.ParentConfirmation;
            account.HealthDataNotice = cmd.HealthDataNotice;
            account.TermsOfUse = cmd.TermsOfUse;
        }
    }
}
