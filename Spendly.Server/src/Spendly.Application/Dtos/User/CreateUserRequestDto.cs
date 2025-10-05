namespace Spendly.Application.Dtos.User
{
    public class CreateUserRequestDto
    {
        // User fields
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // Profile
        public string Firstname { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string Sex { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
    }
}
