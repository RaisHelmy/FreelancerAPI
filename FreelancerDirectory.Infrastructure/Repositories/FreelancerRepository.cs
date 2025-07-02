using Microsoft.EntityFrameworkCore;
using FreelancerDirectory.Application.Interfaces;
using FreelancerDirectory.Domain.Entities;
using FreelancerDirectory.Infrastructure.Data;

namespace FreelancerDirectory.Infrastructure.Repositories
{
    public class FreelancerRepository : IFreelancerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ISkillsetRepository _skillsetRepository;
        private readonly IHobbyRepository _hobbyRepository;

        public FreelancerRepository(
            ApplicationDbContext context,
            ISkillsetRepository skillsetRepository,
            IHobbyRepository hobbyRepository)
        {
            _context = context;
            _skillsetRepository = skillsetRepository;
            _hobbyRepository = hobbyRepository;
        }

        public async Task<IEnumerable<Freelancer>> GetAllAsync()
        {
            return await _context.Freelancers
                .Include(f => f.FreelancerSkillsets)
                    .ThenInclude(fs => fs.Skillset)
                .Include(f => f.FreelancerHobbies)
                    .ThenInclude(fh => fh.Hobby)
                .Where(f => !f.IsArchived)
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

        public async Task<Freelancer> CreateWithSkillsetsAndHobbiesAsync(Freelancer freelancer, List<string> skillsets, List<string> hobbies)
        {
            // Add skillsets
            foreach (var skillsetName in skillsets)
            {
                var skillset = await _skillsetRepository.GetOrCreateAsync(skillsetName);
                freelancer.FreelancerSkillsets.Add(new FreelancerSkillset
                {
                    Freelancer = freelancer,
                    Skillset = skillset
                });
            }

            // Add hobbies
            foreach (var hobbyName in hobbies)
            {
                var hobby = await _hobbyRepository.GetOrCreateAsync(hobbyName);
                freelancer.FreelancerHobbies.Add(new FreelancerHobby
                {
                    Freelancer = freelancer,
                    Hobby = hobby
                });
            }

            _context.Freelancers.Add(freelancer);
            await _context.SaveChangesAsync();
            return freelancer;
        }

        public async Task<Freelancer> UpdateAsync(Freelancer freelancer)
        {
            freelancer.UpdatedAt = DateTime.UtcNow;
            _context.Freelancers.Update(freelancer);
            await _context.SaveChangesAsync();
            return freelancer;
        }

        public async Task<Freelancer> UpdateWithSkillsetsAndHobbiesAsync(Freelancer freelancer, List<string>? skillsets, List<string>? hobbies)
        {
            // Update skillsets if provided
            if (skillsets != null)
            {
                // Remove existing skillsets
                _context.FreelancerSkillsets.RemoveRange(freelancer.FreelancerSkillsets);
                freelancer.FreelancerSkillsets.Clear();

                // Add new skillsets
                foreach (var skillsetName in skillsets)
                {
                    var skillset = await _skillsetRepository.GetOrCreateAsync(skillsetName);
                    freelancer.FreelancerSkillsets.Add(new FreelancerSkillset
                    {
                        Freelancer = freelancer,
                        Skillset = skillset
                    });
                }
            }

            // Update hobbies if provided
            if (hobbies != null)
            {
                // Remove existing hobbies
                _context.FreelancerHobbies.RemoveRange(freelancer.FreelancerHobbies);
                freelancer.FreelancerHobbies.Clear();

                // Add new hobbies
                foreach (var hobbyName in hobbies)
                {
                    var hobby = await _hobbyRepository.GetOrCreateAsync(hobbyName);
                    freelancer.FreelancerHobbies.Add(new FreelancerHobby
                    {
                        Freelancer = freelancer,
                        Hobby = hobby
                    });
                }
            }

            freelancer.UpdatedAt = DateTime.UtcNow;
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
                .Where(f => !f.IsArchived && 
                           (f.Username.Contains(searchTerm) || 
                            f.Email.Contains(searchTerm)))
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
    }
}