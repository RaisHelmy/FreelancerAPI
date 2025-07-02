namespace FreelancerAPI.Domain.Entities
{
    public class Skillset
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        public virtual ICollection<FreelancerSkillset> FreelancerSkillsets { get; set; } = new List<FreelancerSkillset>();
    }
    
    public class FreelancerSkillset
    {
        public int FreelancerId { get; set; }
        public int SkillsetId { get; set; }
        
        public virtual Freelancer Freelancer { get; set; } = null!;
        public virtual Skillset Skillset { get; set; } = null!;
    }
}