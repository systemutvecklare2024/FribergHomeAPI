using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static FribergHomeAPI.Models.PropertyTypes;

namespace FribergHomeAPI.Models
{
    // Author: Christoffer
    public class Property : IEntity
    {
        public int Id { get; set; }

        [Precision(10, 2)]
        [DataType(DataType.Currency)]
        public decimal ListingPrice { get; set; }

        [Precision(10, 2)]
        public decimal LivingSpace { get; set; }

        [Precision(10, 2)]
        public decimal SecondaryArea { get; set; }

        [Precision(10, 2)]
        public decimal LotSize { get; set; }
        public string Description { get; set; }
        public int NumberOfRooms { get; set; }

        [Precision(10, 2)]
        [DataType(DataType.Currency)]
        public decimal MonthlyFee { get; set; }

        [Precision(10, 2)]
        [DataType(DataType.Currency)]
        public decimal OperationalCostPerYear { get; set; }
        public int YearBuilt { get; set; }
        public PropertyType PropertyType { get; set; }

        // Navigation
        public virtual ICollection<PropertyImage> Images { get; set; } 
        public virtual Address Address { get; set; }
        public virtual Muncipality Muncipality { get; set; }
        public virtual RealEstateAgent RealEstateAgent { get; set; }

        public int AddressId { get; set; }
        public int MuncipalityId { get; set; }
        public int RealEstateAgentId { get; set; } = 1;
    }
}
