using account_context.Domain.Aggregates;
using account_context.Domain.Commands;
using Dapr.Client;
using Events;
using MediatR;

namespace account_context.Domain.Handlers
{
    public class CompleteCreatingParentAccountHandler : IRequestHandler<CompleteCreatingParentAccountCommand, bool>
    {
        private readonly DaprClient _daprClient;
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        public CompleteCreatingParentAccountHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(CompleteCreatingParentAccountCommand cmd, CancellationToken cancellationToken)
        {
            var account = await _daprClient.GetStateAsync<Account>(DaprStoreName, cmd.ParentId);

            if (account == null) throw new Exception($"Clinic: {cmd.ParentId} not found.");

            if (account.CurrentState != "InformationAdded")
                throw new Exception($"Account: {cmd.DependentId} is in state: {account.CurrentState}.");

            await _daprClient.PublishEventAsync(DaprPubSubName, "ParentAccountCompleted",
                new ParentAccountCompletedEvent(
                    cmd.ParentId, 
                    cmd.DependentId,
                    cmd.Email,
                    cmd.Password,
                    cmd.SecurityQuestion,
                    cmd.SecurityAnswer,
                    cmd.ParentConfirmation,
                    cmd.HealthDataNotice,
                    cmd.TermsOfUse,
                    account.FirstName + " " + account.LastName));

            return true;
        }
    }
}
