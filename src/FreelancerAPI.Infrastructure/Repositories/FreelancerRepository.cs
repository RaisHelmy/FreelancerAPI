using FreelancerAPI.Application.Interfaces;
using FreelancerAPI.Domain.Entities;
using FreelancerAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FreelancerAPI.Infrastructure.Repositories
{
    public class FreelancerRepository : IFreelancerRepository
    {
        private readonly ApplicationDbContext _context;

        public FreelancerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Freelancer>> GetAllAsync()
        {
            return await _context.Freelancers
                .Include(f => f.FreelancerSkillsets)
                    .ThenInclude(fs => fs.Skillset)
                .Include(f => f.FreelancerHobbies)
                    .ThenInclude(fh => fh.Hobby)
                .ToListAsync();
        }

        public async Task<Freelancer?> GetByIdAsync(int id)
        {
            return await _context.Freelancers
                .Include(f => f.FreelancerSkillsets)
                    .ThenInclude(fs => fs.Skillset)
                .Include(f => f.FreelancerHobbies)
                    .ThenInclude(fh => fh.Hobby)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Freelancer> CreateAsync(Freelancer freelancer)
        {
            _context.Freelancers.Add(freelancer);
            await _context.SaveChangesAsync();
            return freelancer;
        }

        public async Task<Freelancer> UpdateAsync(Freelancer freelancer)
        {
            _context.Freelancers.Update(freelancer);
            await _context.SaveChangesAsync();
            return freelancer;
        }

        public async Task DeleteAsync(int id)
        {
            var freelancer = await _context.Freelancers.FindAsync(id);
            if (freelancer != null)
            {
                _context.Freelancers.Remove(freelancer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Freelancer>> SearchAsync(string searchTerm)
        {
            return await _context.Freelancers
                .Include(f => f.FreelancerSkillsets)
                    .ThenInclude(fs => fs.Skillset)
                .Include(f => f.FreelancerHobbies)
                    .ThenInclude(fh => fh.Hobby)
                .Where(f => f.Username.Contains(searchTerm) || f.Email.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task ArchiveAsync(int id)
        {
            var freelancer = await _context.Freelancers.FindAsync(id);
            if (freelancer != null)
            {
                freelancer.IsArchived = true;
                freelancer.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UnarchiveAsync(int id)
        {
            var freelancer = await _context.Freelancers.FindAsync(id);
            if (freelancer != null)
            {
                freelancer.IsArchived = false;
                freelancer.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<(IEnumerable<Freelancer> data, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            var query = _context.Freelancers
                .Include(f => f.FreelancerSkillsets)
                    .ThenInclude(fs => fs.Skillset)
                .Include(f => f.FreelancerHobbies)
                    .ThenInclude(fh => fh.Hobby);
            // .AsQueryable();

            var totalCount = await query.CountAsync();
            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            
            return (data, totalCount);
        }
    }
}