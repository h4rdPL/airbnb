using airbnb.Domain.Models;

namespace airbnb.Tests.Fixtures
{
    public static class UserFixture
    {
        internal static User CreateTestUser() =>
                new User
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@gmail.com",
                    Password = "string"
                };
    }
}
