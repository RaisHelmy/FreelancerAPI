using FreelancerDirectory.Domain.Entities;

namespace FreelancerDirectory.Application.Interfaces
{
    public interface IFreelancerRepository
    {
        Task<IEnumerable<Freelancer>> GetAllAsync();
        Task<Freelancer?> GetByIdAsync(int id);
        Task<Freelancer> CreateAsync(Freelancer freelancer);
        Task<Freelancer> UpdateAsync(Freelancer freelancer);
        Task DeleteAsync(int id);
        Task<IEnumerable<Freelancer>> SearchAsync(string searchTerm);
        Task ArchiveAsync(int id);
        Task UnarchiveAsync(int id);
    }
}