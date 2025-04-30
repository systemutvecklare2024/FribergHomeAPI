using FribergHomeAPI.Models;
using static FribergHomeAPI.Models.StatusTypes;

namespace FribergHomeAPI.DTOs
{
    public class ApplicationDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public StatusType StatusType { get; set; }
        public int AgencyId { get; set; }
        public int AgentId { get; set; }
    }
}
