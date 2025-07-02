public class FreelancerServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldReturnFreelancerDto_WhenValidInput()
    {
        // Arrange
        var mockRepo = new Mock<IFreelancerRepository>();
        var service = new FreelancerService(mockRepo.Object, null, null);
        
        // Act & Assert
        // Test implementation...
    }
    
    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenFreelancerNotExists()
    {
        // Test implementation...
    }
    
    [Fact]
    public async Task SearchAsync_ShouldReturnMatchingFreelancers_WhenSearchTermProvided()
    {
        // Test implementation...
    }
}