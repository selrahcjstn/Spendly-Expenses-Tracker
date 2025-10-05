namespace Spendly.Application.Dtos.User
{
    public class UpdateEmailRequestDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; }
    }
}
