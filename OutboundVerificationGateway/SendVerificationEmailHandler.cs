using Dapr.Client;
using Events;
using MediatR;
using OutboundVerificationGateway.Domain;

namespace OutboundVerificationGateway
{
    public class SendVerificationEmailHandler : IRequestHandler<SendVerificationEmailCommand, bool>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";
        private readonly DaprClient _daprClient;        


        public SendVerificationEmailHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(SendVerificationEmailCommand request, CancellationToken cancellationToken)
        {
            return true;
        }
    }
}
