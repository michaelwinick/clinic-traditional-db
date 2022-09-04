using account_context.Domain.Aggregates;
using account_context.Domain.Commands;
using Dapr.Client;
using Events;
using MediatR;

namespace account_context.Domain.Handlers
{
    public class CompleteCreatingClinicAccountHandler : IRequestHandler<CompleteCreatingClinicAccountCommand, bool>
    {
        private readonly DaprClient _daprClient;
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        public CompleteCreatingClinicAccountHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(CompleteCreatingClinicAccountCommand cmd, CancellationToken cancellationToken)
        {
            var account = await _daprClient.GetStateAsync<Account>(DaprStoreName, cmd.AccountId);

            if (account == null) throw new Exception($"Clinic: {cmd.ClinicId} not found.");

            if (account.CurrentState != "Started")
                throw new Exception($"Account: {cmd.ClinicId} is in state: {account.CurrentState}.");

            UpdateClinic(cmd, account);
            account.CurrentState = "Completed";

            await _daprClient.SaveStateAsync(DaprStoreName, cmd.AccountId, account);

            await _daprClient.PublishEventAsync(DaprPubSubName, "ClinicAccountCreated",
                new ClinicAccountCreatedEvent(
                    account.AccountId, 
                    account.ClinicId,
                    account.CompanyName,
                    account.Address,
                    account.Country));

            return true;
        }

        private void UpdateClinic(CompleteCreatingClinicAccountCommand command, Account clinicAccount)
        {
            clinicAccount.CompanyName = command.Name;
            clinicAccount.Address = command.Address;
            clinicAccount.Country = command.Country;
            clinicAccount.Region = command.Region;
        }
    }
}
