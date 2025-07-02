using Microsoft.EntityFrameworkCore;
using FreelancerDirectory.Application.Interfaces;
using FreelancerDirectory.Domain.Entities;
using FreelancerDirectory.Infrastructure.Data;

namespace FreelancerDirectory.Infrastructure.Repositories
{
    public class SkillsetRepository : ISkillsetRepository
    {
        private readonly ApplicationDbContext _context;

        public SkillsetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Skillset> GetOrCreateAsync(string name)
        {
            var skillset = await _context.Skillsets.FirstOrDefaultAsync(s => s.Name == name);
            if (skillset == null)
            {
                skillset = new Skillset { Name = name };
                _context.Skillsets.Add(skillset);
                await _context.SaveChangesAsync();
            }
            return skillset;
        }

        public async Task<IEnumerable<Skillset>> GetAllAsync()
        {
            return await _context.Skillsets.ToListAsync();
        }
    }
}