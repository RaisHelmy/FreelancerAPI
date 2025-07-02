namespace FreelancerDirectory.Domain.Entities
{
    public class Hobby
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        public ICollection<FreelancerHobby> FreelancerHobbies { get; set; } = new List<FreelancerHobby>();
    }
}