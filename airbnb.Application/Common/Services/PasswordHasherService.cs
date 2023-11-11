using airbnb.Application.Common.Interfaces;
using System.Security.Cryptography;

namespace airbnb.Application.Common.Services
{
    public class PasswordHasherService : IPasswordHasherService
    {
        private const int SaltSize = 128 / 8;
        private const int KeySize = 256 / 8;
        private const int Iteration = 10000;
        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
        private const char Delimiter = ';';
        public byte[] Salt { get; private set; }
        public byte[] Hash { get; private set; }

        public string HashPassword(string password)
        {
            Salt = RandomNumberGenerator.GetBytes(SaltSize);
            Hash = Rfc2898DeriveBytes.Pbkdf2(password, Salt, Iteration, _hashAlgorithmName, KeySize);

            return string.Join(Delimiter, Convert.ToBase64String(Salt), Convert.ToBase64String(Hash));
        }

        public bool VerifyPassword(string passwordHash, string userPassword)
        {
            throw new NotImplementedException();
        }
    }

}
