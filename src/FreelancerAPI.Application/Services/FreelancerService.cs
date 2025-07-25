using AutoMapper;
using FreelancerAPI.Application.DTOs;
using FreelancerAPI.Application.Interfaces;
using FreelancerAPI.Domain.Entities;

namespace FreelancerAPI.Application.Services
{
    public class FreelancerService : IFreelancerService
    {
        private readonly IFreelancerRepository _freelancerRepository;
        private readonly ISkillsetRepository _skillsetRepository;
        private readonly IHobbyRepository _hobbyRepository;
        private readonly IMapper _mapper;

        public FreelancerService(
            IFreelancerRepository freelancerRepository,
            ISkillsetRepository skillsetRepository,
            IHobbyRepository hobbyRepository,
            IMapper mapper)
        {
            _freelancerRepository = freelancerRepository;
            _skillsetRepository = skillsetRepository;
            _hobbyRepository = hobbyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FreelancerDto>> GetAllAsync()
        {
            var freelancers = await _freelancerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<FreelancerDto>>(freelancers);
        }

        public async Task<FreelancerDto?> GetByIdAsync(int id)
        {
            var freelancer = await _freelancerRepository.GetByIdAsync(id);
            return freelancer == null ? null : _mapper.Map<FreelancerDto>(freelancer);
        }

        public async Task<FreelancerDto> CreateAsync(CreateFreelancerDto dto)
        {
            var freelancer = new Freelancer
            {
                Username = dto.Username,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Handle skillsets
            foreach (var skillsetName in dto.Skillsets)
            {
                var skillset = await _skillsetRepository.GetByNameAsync(skillsetName);
                if (skillset == null)
                {
                    skillset = await _skillsetRepository.CreateAsync(new Skillset { Name = skillsetName });
                }
                freelancer.FreelancerSkillsets.Add(new FreelancerSkillset
                {
                    Freelancer = freelancer,
                    Skillset = skillset
                });
            }

            // Handle hobbies
            foreach (var hobbyName in dto.Hobbies)
            {
                var hobby = await _hobbyRepository.GetByNameAsync(hobbyName);
                if (hobby == null)
                {
                    hobby = await _hobbyRepository.CreateAsync(new Hobby { Name = hobbyName });
                }
                freelancer.FreelancerHobbies.Add(new FreelancerHobby
                {
                    Freelancer = freelancer,
                    Hobby = hobby
                });
            }

            var created = await _freelancerRepository.CreateAsync(freelancer);
            return _mapper.Map<FreelancerDto>(created);
        }

        public async Task<FreelancerDto?> UpdateAsync(int id, UpdateFreelancerDto dto)
        {
            var freelancer = await _freelancerRepository.GetByIdAsync(id);
            if (freelancer == null) return null;

            freelancer.Username = dto.Username;
            freelancer.Email = dto.Email;
            freelancer.PhoneNumber = dto.PhoneNumber;
            freelancer.UpdatedAt = DateTime.UtcNow;

            // Clear existing relationships
            freelancer.FreelancerSkillsets.Clear();
            freelancer.FreelancerHobbies.Clear();

            // Handle skillsets
            foreach (var skillsetName in dto.Skillsets)
            {
                var skillset = await _skillsetRepository.GetByNameAsync(skillsetName);
                if (skillset == null)
                {
                    skillset = await _skillsetRepository.CreateAsync(new Skillset { Name = skillsetName });
                }
                freelancer.FreelancerSkillsets.Add(new FreelancerSkillset
                {
                    FreelancerId = freelancer.Id,
                    SkillsetId = skillset.Id
                });
            }

            // Handle hobbies
            foreach (var hobbyName in dto.Hobbies)
            {
                var hobby = await _hobbyRepository.GetByNameAsync(hobbyName);
                if (hobby == null)
                {
                    hobby = await _hobbyRepository.CreateAsync(new Hobby { Name = hobbyName });
                }
                freelancer.FreelancerHobbies.Add(new FreelancerHobby
                {
                    FreelancerId = freelancer.Id,
                    HobbyId = hobby.Id
                });
            }

            var updated = await _freelancerRepository.UpdateAsync(freelancer);
            return _mapper.Map<FreelancerDto>(updated);
        }

        public async Task DeleteAsync(int id)
        {
            await _freelancerRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<FreelancerDto>> SearchAsync(string searchTerm)
        {
            var freelancers = await _freelancerRepository.SearchAsync(searchTerm);
            return _mapper.Map<IEnumerable<FreelancerDto>>(freelancers);
        }

        public async Task ArchiveAsync(int id)
        {
            await _freelancerRepository.ArchiveAsync(id);
        }

        public async Task UnarchiveAsync(int id)
        {
            await _freelancerRepository.UnarchiveAsync(id);
        }

        public async Task<PagedResult<FreelancerDto>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            var (data, totalCount) = await _freelancerRepository.GetAllPagedAsync(pageNumber, pageSize);
            var freelancerDtos = _mapper.Map<IEnumerable<FreelancerDto>>(data);
            return new PagedResult<FreelancerDto>
            {
                Data = freelancerDtos.ToList(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
            };
        }
    }
}