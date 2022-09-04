using System.Text.Json.Serialization;
using account_context.Controllers;
using account_context.Domain.Aggregates;
using account_context.Domain.Commands;
using Dapr.Client;
using Events;
using MediatR;

namespace account_context.Domain.Handlers
{
    public class StartCreatingEmployeeAccountHandler : IRequestHandler<StartCreatingEmployeeAccountCommand, string>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        private readonly DaprClient _daprClient;


        public StartCreatingEmployeeAccountHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<string> Handle(StartCreatingEmployeeAccountCommand cmd,
            CancellationToken cancellationToken)
        {
            var newAccountId = Guid.NewGuid().ToString();

            var account = UpdateAccount(cmd, newAccountId);

            await _daprClient.SaveStateAsync(DaprStoreName, newAccountId, account);

            await _daprClient.PublishEventAsync(DaprPubSubName, "EmployeeUnverifiedAccountCreated",
                new EmployeeUnverifiedAccountCreatedEvent(
                    newAccountId,
                    cmd.ClinicId,
                    cmd.FirstName,
                    cmd.LastName,
                    cmd.Email
                ));

            return newAccountId;
        }

        private static Account UpdateAccount(StartCreatingEmployeeAccountCommand request, string accountId)
        {
            return new Account(accountId)
            {
                ClinicId = request.ClinicId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Title = request.Title,
                Email = request.Email,
                AccountType = "Employee"
            };
        }
    }
}
