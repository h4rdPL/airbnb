namespace airbnb.Application.Common.Interfaces
{
    public interface IPasswordHasherService
    {
        string HashPassword(string password);
        bool VerifyPassword(string passwordHash, string userPassword);
    }
}
