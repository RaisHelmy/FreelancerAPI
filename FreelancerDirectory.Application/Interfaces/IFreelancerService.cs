using FreelancerDirectory.Application.DTOs;

namespace FreelancerDirectory.Application.Interfaces
{
    public interface IFreelancerService
    {
        Task<IEnumerable<FreelancerDto>> GetAllAsync();
        Task<FreelancerDto?> GetByIdAsync(int id);
        Task<FreelancerDto> CreateAsync(CreateFreelancerDto createDto);
        Task<FreelancerDto> UpdateAsync(int id, UpdateFreelancerDto updateDto);
        Task<FreelancerDto> PatchAsync(int id, UpdateFreelancerDto patchDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<FreelancerDto>> SearchAsync(string searchTerm);
        Task<FreelancerDto> ArchiveAsync(int id);
        Task<FreelancerDto> UnarchiveAsync(int id);
    }
}