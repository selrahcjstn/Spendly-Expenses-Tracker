namespace Spendly.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? RevokedAt { get; set; }
        public bool IsRevoked { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public RefreshToken() { }

        public RefreshToken(User user, string token, int daysToExpire = 7)
        {
            User = user;
            Token = token;
            ExpiresAt = DateTime.UtcNow.AddDays(daysToExpire);
        }

        public void Revoke()
        {
            IsRevoked = true;
            RevokedAt = DateTime.UtcNow;
        }
    }

}
