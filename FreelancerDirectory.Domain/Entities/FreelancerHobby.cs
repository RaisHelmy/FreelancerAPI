namespace FreelancerDirectory.Domain.Entities
{
    public class FreelancerHobby
    {
        public int FreelancerId { get; set; }
        public Freelancer Freelancer { get; set; } = null!;
        
        public int HobbyId { get; set; }
        public Hobby Hobby { get; set; } = null!;
    }
}