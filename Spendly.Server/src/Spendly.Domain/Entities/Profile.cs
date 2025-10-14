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
            Firstname = Capitalize(firstName);
            LastName = Capitalize(lastName);
            MiddleName = middleName != null ? Capitalize(middleName) : null;
            Sex = sex;
            BirthDate = birthDate;
        }

        internal void SetUser(User user)
        {
            User = user;
            UserId = user.Id;
        }

        public void UpdateProfile(string firstName, string lastName, string? middleName, string sex, DateOnly birhtDate)
        {
            Firstname = Capitalize(firstName);
            LastName = Capitalize(lastName);
            MiddleName = middleName != null ? Capitalize(middleName) : null; 
            Sex = sex;
            BirthDate = birhtDate;
        }

        private static string Capitalize(string name)
        {
            name = name.Trim();
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo
                .ToTitleCase(name.ToLower());
        }

    }
}
