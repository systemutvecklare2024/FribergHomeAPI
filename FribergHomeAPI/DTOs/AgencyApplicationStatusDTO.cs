using static FribergHomeAPI.Models.StatusTypes;

namespace FribergHomeAPI.DTOs
{
    public class AgencyApplicationStatusDTO
    {
        public int Id { get; set; }
        public StatusType Status { get; set; }
        public int AgencyId { get; set; }
    }
}
