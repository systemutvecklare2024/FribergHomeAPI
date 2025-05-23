﻿using AutoMapper;
using FribergHomeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FribergHomeAPI.Data.Repositories
{
    public class RealEstateAgentRepository : GenericRepository<RealEstateAgent, ApplicationDbContext>, IRealEstateAgentRepository
    {
        private readonly ApplicationDbContext dbContext;

        public RealEstateAgentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
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
                .Include(a => a.Agency)
                .FirstOrDefaultAsync(a => a.ApiUserId == id);
        }
        public async Task<RealEstateAgent> GetByIdWithApiUser(int id)
        {
            return await dbContext.Agents.Include(a => a.ApiUser).FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<RealEstateAgent> GetbyEmailAsync(string email)
        {
            return await dbContext.Agents.FirstAsync(a => a.Email == email);
        }
    }
}
