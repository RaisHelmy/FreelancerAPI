using FreelancerAPI.Domain.Entities;

namespace FreelancerAPI.Application.Interfaces
{
    public interface IHobbyRepository
    {
        Task<IEnumerable<Hobby>> GetAllAsync();
        Task<Hobby?> GetByNameAsync(string name);
        Task<Hobby> CreateAsync(Hobby hobby);
    }
}