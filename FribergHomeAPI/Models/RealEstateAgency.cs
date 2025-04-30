namespace FribergHomeAPI.Models
{
    // Author: Christoffer
    public class RealEstateAgency : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Presentation { get; set; }
        public string LogoUrl { get; set; }

        // Navigation
        public virtual ICollection<RealEstateAgent> Agents { get; set; }
        public ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}
