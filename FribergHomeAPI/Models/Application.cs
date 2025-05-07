using static FribergHomeAPI.Models.StatusTypes;

namespace FribergHomeAPI.Models
{
    public class Application : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public StatusType StatusType { get; set; } = StatusType.Pending;
        public int AgencyId { get; set; }
        public int AgentId { get; set; }

        //Navigation

        public virtual RealEstateAgency Agency { get; set; }
        public virtual RealEstateAgent Agent { get; set; }
    }
}
