using AutoMapper;
using FribergHomeAPI.DTOs;
using FribergHomeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergHomeAPI.Data.Repositories
{
	// Author: Christoffer
	public class PropertyRepository : GenericRepository<Property, ApplicationDbContext>, IPropertyRepository
	{
		private readonly IMapper _mapper;

		public PropertyRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext)
		{
			_mapper = mapper;
		}

		public async Task<ICollection<Property>?> FindPropertyInMuncipality(Muncipality muncipality)
		{
			return await DbContext
						.Properties
						.Where(e => e.Muncipality == muncipality)
						.ToListAsync();
		}
		//Author: Glate
		public async Task<ICollection<Property>?> GetByMuncipalityId(int muncipalityId)
		{
			return await DbContext
						.Properties
						.Where(e => e.Muncipality.Id == muncipalityId)
						.Include(p => p.Address)
						.Include(p => p.Images)
						.Include(p => p.RealEstateAgent)
						.OrderBy(p => p.Id)
						.ToListAsync();
		}

		public async Task<Property?> GetWithAddressAsync(int id)
		{
			return await DbContext.Set<Property>().Include(p => p.Address).FirstOrDefaultAsync(e => e.Id == id);
		}

		public override async Task<IEnumerable<Property>?> GetAllAsync()
		{
			return await DbContext.Set<Property>()
						.Include(p => p.Address)
						.Include(p => p.Images)
						.Include(p => p.RealEstateAgent)
						.OrderByDescending(p => p.Id)
						.ToListAsync();
		}
		// Fredrik
		public async Task<bool> UpdateAsync(int id, PropertyDTO dto)
		{
			var existingProperty = await DbContext.Properties
				.Include(p => p.Address)
				.Include(p => p.Images)
				.FirstOrDefaultAsync(p => p.Id == id);

			if (existingProperty == null)
			{
				return false; // Returnera false om entiteten inte hittas
			}

			// Uppdatera entiteten med DTO-data
			_mapper.Map(dto, existingProperty);

			DbContext.Properties.Update(existingProperty);
			await DbContext.SaveChangesAsync();

			return true;
		}
		//Glate
		public async Task UpdateAsync(Property property, List<PropertyImage> imagesToDelete)
		{
			if (imagesToDelete.Count > 0)
			{
				DbContext.PropertyImages.RemoveRange(imagesToDelete);
			}
			DbContext.Properties.Update(property);
			await DbContext.SaveChangesAsync();
		}

		//Author: Glate
		public async Task<IEnumerable<Property>?> GetLatestAsync(int take)
		{
			return await DbContext.Set<Property>()
						 .Include(p => p.Address)
						 .Include(p => p.Images)
						 .Include(p => p.RealEstateAgent)
						 .OrderByDescending(p => p.Id)
						 .Take(take)
						 .ToListAsync();
		}

        public async Task<Property?> GetWithAddressImagesAndMuncipality(int id)
        {
            return await DbContext.Set<Property>()
                .Include(p => p.Address)
                .Include(p => p.Images)
				.Include(p => p.Muncipality)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Property>?> GetAllPropertiesByAgentIdAsync(int agentId)
        {
            return await DbContext.Set<Property>()
                .Include(p => p.Address)
                .Include(p => p.Images)
                .Where(p => p.RealEstateAgentId == agentId)
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }
    }
}
