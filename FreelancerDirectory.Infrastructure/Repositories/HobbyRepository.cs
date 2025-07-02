using Microsoft.EntityFrameworkCore;
using FreelancerDirectory.Application.Interfaces;
using FreelancerDirectory.Domain.Entities;
using FreelancerDirectory.Infrastructure.Data;

namespace FreelancerDirectory.Infrastructure.Repositories
{
    public class HobbyRepository : IHobbyRepository
    {
        private readonly ApplicationDbContext _context;

        public HobbyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Hobby> GetOrCreateAsync(string name)
        {
            var hobby = await _context.Hobbies.FirstOrDefaultAsync(h => h.Name == name);
            if (hobby == null)
            {
                hobby = new Hobby { Name = name };
                _context.Hobbies.Add(hobby);
                await _context.SaveChangesAsync();
            }
            return hobby;
        }

        public async Task<IEnumerable<Hobby>> GetAllAsync()
        {
            return await _context.Hobbies.ToListAsync();
        }
    }
}