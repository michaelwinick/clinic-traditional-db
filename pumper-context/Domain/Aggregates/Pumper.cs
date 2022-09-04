using System.Text.Json.Serialization;

namespace pumper_context.Domain.Aggregates
{
    public class Pumper
    {
        public string PumperId { get; init; }
        public string ParentId { set; get; }
        public HashSet<string> HcpAssigned { get; set; } = new();
        public HashSet<string> HcpRequests { get; set; } = new();

        public Pumper()
        {
            
        }

        public Pumper(string dependentId, string parentId)
        {
            PumperId = dependentId;
            ParentId = parentId;
        }

        public Pumper(string pumperId)
        {
            PumperId = pumperId;
        }
    }
}
