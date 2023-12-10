using airbnb.Application.Common.Interfaces;
using System.Security.Cryptography;

namespace airbnb.Application.Services
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

        /// <summary>
        /// Hashes the given password using a secure algorithm.
        /// </summary>
        /// <param name="password">The password to be hashed.</param>
        /// <returns>The hashed password with salt, formatted as a string.</returns>
        public string HashPassword(string password)
        {
            // Generate a random salt.
            Salt = RandomNumberGenerator.GetBytes(SaltSize);

            // Derive a hash using the provided password and generated salt.
            Hash = Rfc2898DeriveBytes.Pbkdf2(password, Salt, Iteration, _hashAlgorithmName, KeySize);

            // Combine salt and hash and return as a string.
            return string.Join(Delimiter, Convert.ToBase64String(Salt), Convert.ToBase64String(Hash));
        }

        /// <summary>
        /// Verifies whether the provided user password matches the hashed password.
        /// </summary>
        /// <param name="passwordHash">The hashed password containing salt.</param>
        /// <param name="userPassword">The user's input password for verification.</param>
        /// <returns>True if the passwords match; otherwise, false.</returns>
        public bool VerifyPassword(string passwordHash, string userPassword)
        {
            // Split the passwordHash into salt and hash.
            var element = passwordHash.Split(Delimiter);
            var salt = Convert.FromBase64String(element[0]);
            var hash = Convert.FromBase64String(element[1]);

            // Derive a hash from the user's input password and stored salt.
            var hashInput = Rfc2898DeriveBytes.Pbkdf2(userPassword, salt, Iteration, _hashAlgorithmName, KeySize);

            // Compare the derived hash with the stored hash in constant time.
            return CryptographicOperations.FixedTimeEquals(hash, hashInput);
        }
    }


}
