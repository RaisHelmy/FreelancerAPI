using FreelancerAPI.Application.Interfaces;
using FreelancerAPI.Domain.Entities;
using FreelancerAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FreelancerAPI.Infrastructure.Repositories
{
    public class SkillsetRepository : ISkillsetRepository
    {
        private readonly ApplicationDbContext _context;

        public SkillsetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Skillset>> GetAllAsync()
        {
            return await _context.Skillsets.ToListAsync();
        }

        public async Task<Skillset?> GetByNameAsync(string name)
        {
            return await _context.Skillsets.FirstOrDefaultAsync(s => s.Name == name);
        }

        public async Task<Skillset> CreateAsync(Skillset skillset)
        {
            _context.Skillsets.Add(skillset);
            await _context.SaveChangesAsync();
            return skillset;
        }
    }
}