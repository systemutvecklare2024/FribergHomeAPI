using FribergHomeAPI.Models;

namespace FribergHomeAPI.DTOs
{
    public class RealEstateAgencyDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Presentation { get; set; }
        public string LogoUrl { get; set; }
        
        //Navigation
        public List<RealEstateAgentDTO?> Agents { get; set; }
    }
}
