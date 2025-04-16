namespace FribergHomeAPI.Models
{
	//Author: Glate
	public class PropertyImage : IEntity
	{
		public int Id { get; set; }
		public string ImgURL { get; set; } 


		public virtual Property Property { get; set; }
		public int PropertyId { get; set; }
	}
}
