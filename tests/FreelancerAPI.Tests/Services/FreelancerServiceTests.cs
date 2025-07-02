using AutoMapper;
using FreelancerAPI.Application.DTOs;
using FreelancerAPI.Application.Interfaces;
using FreelancerAPI.Application.Mappings;
using FreelancerAPI.Application.Services;
using FreelancerAPI.Domain.Entities;
using Moq;
using Xunit;

namespace FreelancerAPI.Tests.Services
{
    public class FreelancerServiceTests
    {
        private readonly Mock<IFreelancerRepository> _mockFreelancerRepo;
        private readonly Mock<ISkillsetRepository> _mockSkillsetRepo;
        private readonly Mock<IHobbyRepository> _mockHobbyRepo;
        private readonly IMapper _mapper;
        private readonly FreelancerService _service;

        public FreelancerServiceTests()
        {
            _mockFreelancerRepo = new Mock<IFreelancerRepository>();
            _mockSkillsetRepo = new Mock<ISkillsetRepository>();
            _mockHobbyRepo = new Mock<IHobbyRepository>();
            
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = configuration.CreateMapper();
            
            _service = new FreelancerService(
                _mockFreelancerRepo.Object,
                _mockSkillsetRepo.Object,
                _mockHobbyRepo.Object,
                _mapper);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnFreelancerDto_WhenValidInput()
        {
            // Arrange
            var createDto = new CreateFreelancerDto
            {
                Username = "johndoe",
                Email = "john@example.com",
                PhoneNumber = "123456789",
                Skillsets = new List<string> { "C#", "JavaScript" },
                Hobbies = new List<string> { "Reading", "Gaming" }
            };

            var skillset1 = new Skillset { Id = 1, Name = "C#" };
            var skillset2 = new Skillset { Id = 2, Name = "JavaScript" };
            var hobby1 = new Hobby { Id = 1, Name = "Reading" };
            var hobby2 = new Hobby { Id = 2, Name = "Gaming" };

            _mockSkillsetRepo.Setup(x => x.GetByNameAsync("C#")).ReturnsAsync(skillset1);
            _mockSkillsetRepo.Setup(x => x.GetByNameAsync("JavaScript")).ReturnsAsync(skillset2);
            _mockHobbyRepo.Setup(x => x.GetByNameAsync("Reading")).ReturnsAsync(hobby1);
            _mockHobbyRepo.Setup(x => x.GetByNameAsync("Gaming")).ReturnsAsync(hobby2);

            var createdFreelancer = new Freelancer
            {
                Id = 1,
                Username = "johndoe",
                Email = "john@example.com",
                PhoneNumber = "123456789",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _mockFreelancerRepo.Setup(x => x.CreateAsync(It.IsAny<Freelancer>()))
                .ReturnsAsync(createdFreelancer);

            // Act
            var result = await _service.CreateAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("johndoe", result.Username);
            Assert.Equal("john@example.com", result.Email);
            Assert.Equal("123456789", result.PhoneNumber);
            _mockFreelancerRepo.Verify(x => x.CreateAsync(It.IsAny<Freelancer>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenFreelancerNotExists()
        {
            // Arrange
            int nonExistentId = 999;
            _mockFreelancerRepo.Setup(x => x.GetByIdAsync(nonExistentId))
                .ReturnsAsync((Freelancer?)null);

            // Act
            var result = await _service.GetByIdAsync(nonExistentId);

            // Assert
            Assert.Null(result);
            _mockFreelancerRepo.Verify(x => x.GetByIdAsync(nonExistentId), Times.Once);
        }

        [Fact]
        public async Task SearchAsync_ShouldReturnMatchingFreelancers_WhenSearchTermProvided()
        {
            // Arrange
            string searchTerm = "john";
            var freelancers = new List<Freelancer>
            {
                new Freelancer
                {
                    Id = 1,
                    Username = "johndoe",
                    Email = "john@example.com",
                    PhoneNumber = "123456789",
                    FreelancerSkillsets = new List<FreelancerSkillset>(),
                    FreelancerHobbies = new List<FreelancerHobby>()
                },
                new Freelancer
                {
                    Id = 2,
                    Username = "johnsmith",
                    Email = "johnsmith@example.com",
                    PhoneNumber = "987654321",
                    FreelancerSkillsets = new List<FreelancerSkillset>(),
                    FreelancerHobbies = new List<FreelancerHobby>()
                }
            };

            _mockFreelancerRepo.Setup(x => x.SearchAsync(searchTerm))
                .ReturnsAsync(freelancers);

            // Act
            var result = await _service.SearchAsync(searchTerm);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, f => Assert.Contains("john", f.Username.ToLower()));
            _mockFreelancerRepo.Verify(x => x.SearchAsync(searchTerm), Times.Once);
        }
    }
}