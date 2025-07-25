using FreelancerAPI.Application.DTOs;

namespace FreelancerAPI.Application.Interfaces
{
    public interface IFreelancerService
    {
        Task<IEnumerable<FreelancerDto>> GetAllAsync();
        Task<FreelancerDto?> GetByIdAsync(int id);
        Task<FreelancerDto> CreateAsync(CreateFreelancerDto dto);
        Task<FreelancerDto?> UpdateAsync(int id, UpdateFreelancerDto dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<FreelancerDto>> SearchAsync(string searchTerm);
        Task ArchiveAsync(int id);
        Task UnarchiveAsync(int id);

        Task<PagedResult<FreelancerDto>> GetAllPagedAsync(int pageNumber, int pageSize);
    }
}