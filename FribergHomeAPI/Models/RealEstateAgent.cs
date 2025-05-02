namespace FribergHomeAPI.Models
{
    // Author: Christoffer
    public class RealEstateAgent : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageUrl { get; set; }

        // Navigation
        public virtual RealEstateAgency Agency { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
        public virtual Application Application { get; set; }

        public int? AgencyId { get; set; }

        public ApiUser ApiUser { get; set; }
        public string? ApiUserId { get; set; }
    }
}
