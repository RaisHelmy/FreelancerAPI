namespace FreelancerAPI.Domain.Entities
{
    public class Freelancer
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsArchived { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public virtual ICollection<FreelancerSkillset> FreelancerSkillsets { get; set; } = new List<FreelancerSkillset>();
        public virtual ICollection<FreelancerHobby> FreelancerHobbies { get; set; } = new List<FreelancerHobby>();
    }
}