namespace FreelancerDirectory.Domain.Entities
{
    public class Skillset
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        public ICollection<FreelancerSkillset> FreelancerSkillsets { get; set; } = new List<FreelancerSkillset>();
    }
}