namespace FreelancerAPI.Application.DTOs
{
    public class FreelancerDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsArchived { get; set; }
        public List<string> Skillsets { get; set; } = new();
        public List<string> Hobbies { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateFreelancerDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public List<string> Skillsets { get; set; } = new();
        public List<string> Hobbies { get; set; } = new();
    }

    public class UpdateFreelancerDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public List<string> Skillsets { get; set; } = new();
        public List<string> Hobbies { get; set; } = new();
    }

    public class PagedResult<T>
    {
        public List<T> Data { get; set; } = new();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;
    }
}