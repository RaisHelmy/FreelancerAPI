namespace FreelancerDirectory.Domain.Entities
{
    public class FreelancerSkillset
    {
        public int FreelancerId { get; set; }
        public Freelancer Freelancer { get; set; } = null!;
        
        public int SkillsetId { get; set; }
        public Skillset Skillset { get; set; } = null!;
    }
}