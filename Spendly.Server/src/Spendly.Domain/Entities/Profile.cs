namespace Spendly.Domain.Entities
{
    public class Profile
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } 
        public string? Bio { get; set; }
        public string Sex { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }

        // Profile one to one to user
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        private Profile() { }

        public Profile(string firstName, string lastName, string? middleName, string sex, DateOnly birthDate)
        {
            Id = Guid.NewGuid();
            Firstname = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Sex = sex;
            BirthDate = birthDate;
        }

        internal void SetUser(User user)
        {
            User = user;
            UserId = user.Id;
        }
    }
}
