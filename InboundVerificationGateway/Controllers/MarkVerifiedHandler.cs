using Dapr.Client;
using Events;
using MediatR;

namespace InboundVerificationGateway.Controllers
{
    public class MarkVerifiedHandler : IRequestHandler<MarkVerifiedCommand, bool>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";
        private readonly DaprClient _daprClient;


        public MarkVerifiedHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(MarkVerifiedCommand cmd, CancellationToken cancellationToken)
        {
            await _daprClient.PublishEventAsync(DaprPubSubName, "EmployeeAccountVerified",
                new EmployeeAccountVerifiedEvent(
                    cmd.AccountId,
                    cmd.Password,
                    cmd.SecurityQuestion,
                    cmd.SecurityAnswer));

            return true;
        }
    }
}
