using FreelancerDirectory.Domain.Entities;

namespace FreelancerDirectory.Application.Interfaces
{
    public interface IFreelancerRepository
    {
        Task<IEnumerable<Freelancer>> GetAllAsync();
        Task<Freelancer?> GetByIdAsync(int id);
        Task<Freelancer> CreateAsync(Freelancer freelancer);
        Task<Freelancer> CreateWithSkillsetsAndHobbiesAsync(Freelancer freelancer, List<string> skillsets, List<string> hobbies);
        Task<Freelancer> UpdateAsync(Freelancer freelancer);
        Task<Freelancer> UpdateWithSkillsetsAndHobbiesAsync(Freelancer freelancer, List<string>? skillsets, List<string>? hobbies);
        Task DeleteAsync(int id);
        Task<IEnumerable<Freelancer>> SearchAsync(string searchTerm);
        Task ArchiveAsync(int id);
        Task UnarchiveAsync(int id);
    }
}