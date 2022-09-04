using Dapr.Client;
using Events;
using MediatR;

namespace ClinicInvitation.Controllers
{
    public class CreateInvitationHandler : IRequestHandler<CreateInvitationCommand, bool>
    {
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";
        private readonly DaprClient _daprClient;


        public CreateInvitationHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(CreateInvitationCommand cmd, CancellationToken cancellationToken)
        {
            var invitationId = cmd.InvitationId;

            var stateAsync = await _daprClient.GetStateAsync<Invitation>(DaprStoreName, invitationId);

            if (stateAsync == null)
                stateAsync = new Invitation(invitationId);
            else
                invitationId = stateAsync.InvitationId + Guid.NewGuid();

            //await _daprClient.SaveStateAsync(DaprStoreName, invitationId, stateAsync);

            await _daprClient.PublishEventAsync(DaprPubSubName, "InvitationCreated",
                new InvitationCreatedEvent(cmd.InvitationId));

            return true;
        }

        
    }
}
