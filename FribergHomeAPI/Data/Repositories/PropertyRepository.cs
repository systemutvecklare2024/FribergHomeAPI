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
                        .OrderBy(p => p.Id)
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
    }
}
