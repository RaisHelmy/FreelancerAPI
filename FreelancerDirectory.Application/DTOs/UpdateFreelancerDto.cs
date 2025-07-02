using System.ComponentModel.DataAnnotations;

namespace FreelancerDirectory.Application.DTOs
{
    public class UpdateFreelancerDto
    {
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        public string? Username { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format")]
        public string? PhoneNumber { get; set; }

        public List<string>? Skillsets { get; set; }
        public List<string>? Hobbies { get; set; }
    }
}