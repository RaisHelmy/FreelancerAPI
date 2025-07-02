namespace FreelancerAPI.Domain.Entities
{
    public class Hobby
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        public virtual ICollection<FreelancerHobby> FreelancerHobbies { get; set; } = new List<FreelancerHobby>();
    }
    
    public class FreelancerHobby
    {
        public int FreelancerId { get; set; }
        public int HobbyId { get; set; }
        
        public virtual Freelancer Freelancer { get; set; } = null!;
        public virtual Hobby Hobby { get; set; } = null!;
    }
}