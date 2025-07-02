namespace FreelancerDirectory.Domain.Entities
{
    public class Freelancer
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsArchived { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties
        public ICollection<FreelancerSkillset> FreelancerSkillsets { get; set; } = new List<FreelancerSkillset>();
        public ICollection<FreelancerHobby> FreelancerHobbies { get; set; } = new List<FreelancerHobby>();
    }
}