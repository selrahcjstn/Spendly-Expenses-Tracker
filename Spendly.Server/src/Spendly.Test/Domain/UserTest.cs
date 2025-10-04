using Spendly.Domain.Entities;
using Xunit;

namespace Spendly.Test.Domain
{
    public class UserTest
    {
        [Fact]
        public void UpdateEmail_Should_Update_When_ValidEmail()
        {
            // Arrange
            var user = new User("Charles", "charles@gmail.com", "Charles22.");

            // Act
            user.UpdateEmail("newtes@gmail.com");

            // Assert
            Assert.Equal("newtes@gmail.com", user.Email);
            Assert.NotNull(user.Email);
        }

        [Fact]
        public void UpdateEmail_Should_Throw_When_Invalid_Email()
        {
            // Arrange
            var user = new User("Charles", "charles@gmail.com", "Charles22.");

            // Assert
            Assert.Throws<ArgumentException>(() => user.UpdateEmail("invalid-email"));
        }
    }
}
