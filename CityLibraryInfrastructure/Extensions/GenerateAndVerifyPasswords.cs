using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityLibraryInfrastructure.Extensions
{
    public static class GenerateAndVerifyPasswords
    {
        private static readonly byte MinimumPasswordLength = 8;
        public static void CreatePasswordHash(this string password, out string passwordHash)
        {
            //TODO general exception
            if (password == null)
                throw new ArgumentNullException(nameof(password), "Password could not be null");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password must not contain space character", nameof(password));
            if (password.Length < MinimumPasswordLength)
                throw new ArgumentException($"Your password must compose of minimum {MinimumPasswordLength} characters", nameof(password));

            passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password, hashType: HashType.SHA384);
        }

        public static bool VerifyPasswordHash(this string password, string passwordHash)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password), "Password could not be null");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password must not contain space character", nameof(password));

            return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash, hashType: HashType.SHA384);
        }
    }
}
