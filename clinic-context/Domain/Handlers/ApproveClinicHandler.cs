using clinic_context.Controllers;
using clinic_context.Domain.Aggregates;
using clinic_context.Domain.Commands;
using Dapr.Client;
using Events;
using MediatR;


namespace clinic_context.Domain.Handlers
{
    public class ApproveClinicHandler : IRequestHandler<ApproveClinicCommand, bool>
    {
        private readonly DaprClient _daprClient;
        private const string DaprStoreName = "statestore";
        private const string DaprPubSubName = "pubsub";

        public ApproveClinicHandler(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public async Task<bool> Handle(ApproveClinicCommand cmd, CancellationToken cancellationToken)
        {
            var clinic = await _daprClient.GetStateAsync<Clinic>(DaprStoreName, cmd.ClinicId);

            if (clinic == null) 
                return true;

            clinic.ApprovedPatients.Add(cmd.PatientId);
            clinic.OutstandingAccessRequests.Remove(cmd.PatientId);

            await _daprClient.SaveStateAsync(DaprStoreName, cmd.ClinicId, clinic);

            await _daprClient.PublishEventAsync(DaprPubSubName, "ClinicAccessApproved",
                new ClinicAccessApprovedEvent(clinic.ClinicId, cmd.PatientId));

            return true;
        }
    }
}
