namespace Spendly.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public Profile? Profile { get; set; }

        public User() { }

        public User(string username, string email, string password)
        {
            Id = Guid.NewGuid();
            Username = username;
            Email = email;
            Password = password;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddProfile(Profile profile)
        {
            Profile = profile;
            Profile.SetUser(this);
        }

        public void UpdateEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || !email.Contains('@'))
                throw new ArgumentException("Invalid email format!");

            Email = email;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
