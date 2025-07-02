using FreelancerDirectory.Domain.Entities;

namespace FreelancerDirectory.Application.Interfaces
{
    public interface IHobbyRepository
    {
        Task<Hobby> GetOrCreateAsync(string name);
        Task<IEnumerable<Hobby>> GetAllAsync();
    }
}