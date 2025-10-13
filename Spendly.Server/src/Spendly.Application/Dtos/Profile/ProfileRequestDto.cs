namespace Spendly.Application.Dtos.Profile
{
    public class ProfileRequestDto
    {
        public string Firstname { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string Sex { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
    }
}