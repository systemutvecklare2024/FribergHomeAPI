namespace FribergHomeAPI.Models
{
    // Author: Christoffer
    public class Address : IEntity
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string County { get; set; } = ""; 
        public string PostalCode { get; set; }
        public string Country { get; set; } = "";

        public virtual ICollection<Property> Properties { get; set; }
    }
}