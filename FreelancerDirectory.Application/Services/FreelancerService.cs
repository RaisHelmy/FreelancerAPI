using FreelancerDirectory.Application.DTOs;
using FreelancerDirectory.Application.Interfaces;
using FreelancerDirectory.Domain.Entities;

namespace FreelancerDirectory.Application.Services
{
    public class FreelancerService : IFreelancerService
    {
        private readonly IFreelancerRepository _repository;
        private readonly ISkillsetRepository _skillsetRepository;
        private readonly IHobbyRepository _hobbyRepository;

        public FreelancerService(
            IFreelancerRepository repository, 
            ISkillsetRepository skillsetRepository,
            IHobbyRepository hobbyRepository)
        {
            _repository = repository;
            _skillsetRepository = skillsetRepository;
            _hobbyRepository = hobbyRepository;
        }

        public async Task<IEnumerable<FreelancerDto>> GetAllAsync()
        {
            var freelancers = await _repository.GetAllAsync();
            return freelancers.Select(MapToDto);
        }

        public async Task<FreelancerDto?> GetByIdAsync(int id)
        {
            var freelancer = await _repository.GetByIdAsync(id);
            return freelancer != null ? MapToDto(freelancer) : null;
        }

        public async Task<FreelancerDto> CreateAsync(CreateFreelancerDto createDto)
        {
            var freelancer = new Freelancer
            {
                Username = createDto.Username,
                Email = createDto.Email,
                PhoneNumber = createDto.PhoneNumber,
                CreatedAt = DateTime.UtcNow
            };

            // Let repository handle the skillsets and hobbies
            var createdFreelancer = await _repository.CreateWithSkillsetsAndHobbiesAsync(
                freelancer, createDto.Skillsets, createDto.Hobbies);
                
            return MapToDto(createdFreelancer);
        }

        public async Task<FreelancerDto> UpdateAsync(int id, UpdateFreelancerDto updateDto)
        {
            var freelancer = await _repository.GetByIdAsync(id);
            if (freelancer == null)
                throw new KeyNotFoundException($"Freelancer with ID {id} not found");

            // Update basic properties
            if (!string.IsNullOrEmpty(updateDto.Username))
                freelancer.Username = updateDto.Username;
            
            if (!string.IsNullOrEmpty(updateDto.Email))
                freelancer.Email = updateDto.Email;
            
            if (!string.IsNullOrEmpty(updateDto.PhoneNumber))
                freelancer.PhoneNumber = updateDto.PhoneNumber;

            // Let repository handle skillsets and hobbies update
            var updatedFreelancer = await _repository.UpdateWithSkillsetsAndHobbiesAsync(
                freelancer, updateDto.Skillsets, updateDto.Hobbies);
                
            return MapToDto(updatedFreelancer);
        }

        public async Task<FreelancerDto> PatchAsync(int id, UpdateFreelancerDto patchDto)
        {
            return await UpdateAsync(id, patchDto);
        }

        public async Task DeleteAsync(int id)
        {
            var freelancer = await _repository.GetByIdAsync(id);
            if (freelancer == null)
                throw new KeyNotFoundException($"Freelancer with ID {id} not found");

            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<FreelancerDto>> SearchAsync(string searchTerm)
        {
            var freelancers = await _repository.SearchAsync(searchTerm);
            return freelancers.Select(MapToDto);
        }

        public async Task<FreelancerDto> ArchiveAsync(int id)
        {
            var freelancer = await _repository.GetByIdAsync(id);
            if (freelancer == null)
                throw new KeyNotFoundException($"Freelancer with ID {id} not found");

            await _repository.ArchiveAsync(id);
            freelancer.IsArchived = true;
            return MapToDto(freelancer);
        }

        public async Task<FreelancerDto> UnarchiveAsync(int id)
        {
            var freelancer = await _repository.GetByIdAsync(id);
            if (freelancer == null)
                throw new KeyNotFoundException($"Freelancer with ID {id} not found");

            await _repository.UnarchiveAsync(id);
            freelancer.IsArchived = false;
            return MapToDto(freelancer);
        }

        private static FreelancerDto MapToDto(Freelancer freelancer)
        {
            return new FreelancerDto
            {
                Id = freelancer.Id,
                Username = freelancer.Username,
                Email = freelancer.Email,
                PhoneNumber = freelancer.PhoneNumber,
                IsArchived = freelancer.IsArchived,
                CreatedAt = freelancer.CreatedAt,
                UpdatedAt = freelancer.UpdatedAt,
                Skillsets = freelancer.FreelancerSkillsets.Select(fs => fs.Skillset.Name).ToList(),
                Hobbies = freelancer.FreelancerHobbies.Select(fh => fh.Hobby.Name).ToList()
            };
        }
    }
}