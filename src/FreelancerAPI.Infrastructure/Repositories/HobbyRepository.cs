using FreelancerAPI.Application.Interfaces;
using FreelancerAPI.Domain.Entities;
using FreelancerAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FreelancerAPI.Infrastructure.Repositories
{
    public class HobbyRepository : IHobbyRepository
    {
        private readonly ApplicationDbContext _context;

        public HobbyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hobby>> GetAllAsync()
        {
            return await _context.Hobbies.ToListAsync();
        }

        public async Task<Hobby?> GetByNameAsync(string name)
        {
            return await _context.Hobbies.FirstOrDefaultAsync(h => h.Name == name);
        }

        public async Task<Hobby> CreateAsync(Hobby hobby)
        {
            _context.Hobbies.Add(hobby);
            await _context.SaveChangesAsync();
            return hobby;
        }
    }
}