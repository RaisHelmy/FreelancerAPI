using FreelancerAPI.Domain.Entities;

namespace FreelancerAPI.Application.Interfaces
{
    public interface ISkillsetRepository
    {
        Task<IEnumerable<Skillset>> GetAllAsync();
        Task<Skillset?> GetByNameAsync(string name);
        Task<Skillset> CreateAsync(Skillset skillset);
    }
}