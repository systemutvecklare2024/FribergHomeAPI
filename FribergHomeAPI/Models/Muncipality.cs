namespace FribergHomeAPI.Models
{
    // Author: Christoffer
    public class Muncipality : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation
        public virtual ICollection<Property> Properties { get; set; }
    }
}
