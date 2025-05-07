using AutoMapper;
using FribergHomeAPI.Models;
using FribergHomeAPI.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FribergHomeAPI.Data.Repositories
{
    public class RealEstateAgentRepository : GenericRepository<RealEstateAgent, ApplicationDbContext>, IRealEstateAgentRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public RealEstateAgentRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<RealEstateAgent>> GetAllAgentsAsync()
        {
            return await dbContext.Agents
                .Include(a => a.Agency)
                .Include(p => p.Properties)
                .ToListAsync();
        }

        //Tobias
        public async Task<RealEstateAgent?> GetByIdWithAgencyAsync(int id)
        {
            return await dbContext.Agents
                .Include(a => a.Agency)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        //Tobias
        public async Task<RealEstateAgent?> GetApiUserIdAsync(string id)
        {
            return await dbContext.Agents
                .FirstOrDefaultAsync(a => a.ApiUserId == id);
        }
        public async Task UpdateAgentAsync(int id, UpdateAgentDTO dto) 
        {
            var existingAgent = await dbContext.Agents.FirstOrDefaultAsync(a => a.Id == id);
            
            mapper.Map(dto, existingAgent);
            
            dbContext.Agents.Update(existingAgent);
            await dbContext.SaveChangesAsync();

            
        }
    }
}
