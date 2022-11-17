using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityLibraryInfrastructure.Resources;
using Microsoft.Extensions.Localization;

namespace CityLibraryInfrastructure.Extensions
{
    public static class GenerateAndVerifyPasswords
    {
        private const byte MinimumPasswordLength = 8;

        public static IStringLocalizer<ExceptionsResource> Localizer;
        public static void CreatePasswordHash(this string password, out string passwordHash)
        {
            //TODO general exception
            if (password == null)
                throw new ArgumentNullException(nameof(password), Localizer["Password_Null"]);
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException(Localizer["Password_Space"], nameof(password));
            if (password.Length < MinimumPasswordLength)
                throw new ArgumentException( string.Format(Localizer["Password_Min_Length"], MinimumPasswordLength), nameof(password));

            passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password, hashType: HashType.SHA384);
        }

        public static bool VerifyPasswordHash(this string password, string passwordHash)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password), Localizer["Password_Null"]);
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException(Localizer["Password_Space"], nameof(password));

            return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash, hashType: HashType.SHA384);
        }
    }
}
