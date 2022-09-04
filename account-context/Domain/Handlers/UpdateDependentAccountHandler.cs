using System.Text.Json.Serialization;
using account_context.Controllers;
using account_context.Domain.Aggregates;
using account_context.Domain.Commands;
using Dapr.Client;
using Events;
using MediatR;

namespace account_context.Domain.Handlers
{
    public class UpdateDependentAccountHandler : IRequestHandler<UpdateDependentAccountCommand, bool>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";
        private readonly DaprClient _daprClient;


        public UpdateDependentAccountHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(UpdateDependentAccountCommand cmd, CancellationToken cancellationToken)
        {
            var @event = cmd.Event;

            var account = await _daprClient.GetStateAsync<Account>(DaprStoreName, @event.AccountId) ?? 
                          new Account(@event.AccountId);

            UpdateDependent(@event, account);

            await _daprClient.SaveStateAsync(DaprStoreName, @event.AccountId, account);

            return true;
        }

        private void UpdateDependent(DependentAccountCreatedEvent cmd, Account account)
        {
            account.CurrentState = "Completed";
            account.AccountType = "Dependent";
            account.ParentId = cmd.ParentId;
            account.FirstName = cmd.DependentFirstName;
            account.Title = cmd.Title;
            account.LastName = cmd.DependentLastName;
            account.Month = cmd.Month;
            account.Day = cmd.Day;
            account.Year = cmd.Year;
        }
    }
}
