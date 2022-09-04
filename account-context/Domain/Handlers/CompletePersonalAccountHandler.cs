using account_context.Domain.Aggregates;
using account_context.Domain.Commands;
using Dapr.Client;
using Events;
using MediatR;

namespace account_context.Domain.Handlers
{
    public class CompletePersonalAccountHandler : IRequestHandler<CompletePersonalAccountCommand, bool>
    {
        private readonly DaprClient _daprClient;
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";


        public CompletePersonalAccountHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(CompletePersonalAccountCommand cmd, CancellationToken cancellationToken)
        {
            var stateAsync = await _daprClient.GetStateAsync<Account>(DaprStoreName, cmd.AccountId);
            if (stateAsync == null) return false;

            await _daprClient.PublishEventAsync(DaprPubSubName, "PersonalAccountCreated",
                new PersonalAccountCreatedEvent(
                    cmd.AccountId,
                    cmd.Email,
                    cmd.Password,
                    cmd.SecurityQuestion,
                    cmd.SecurityAnswer,
                    cmd.HealthDataNotice,
                    cmd.TermsOfUse,
                    stateAsync.FirstName,
                    stateAsync.LastName,
                    stateAsync.Dob));

            return true;
        }
    }
}
