using FreelancerDirectory.Domain.Entities;

namespace FreelancerDirectory.Application.Interfaces
{
    public interface ISkillsetRepository
    {
        Task<Skillset> GetOrCreateAsync(string name);
        Task<IEnumerable<Skillset>> GetAllAsync();
    }
}