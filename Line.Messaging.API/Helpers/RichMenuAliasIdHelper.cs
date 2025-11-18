using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Line.Messaging.API.Helpers
{
    /// <summary>
    /// Helper class for generating and validating rich menu alias IDs
    /// </summary>
    public static class RichMenuAliasIdHelper
    {
        /// <summary>
        /// Valid characters for rich menu alias ID: a-z, 0-9, _, -
        /// </summary>
        private const string ValidChars = "abcdefghijklmnopqrstuvwxyz0123456789_-";

        /// <summary>
        /// Maximum length for rich menu alias ID
        /// </summary>
        public const int MaxLength = 32;

        /// <summary>
        /// Minimum length for rich menu alias ID
        /// </summary>
        public const int MinLength = 1;

        /// <summary>
        /// Regular expression pattern for validating rich menu alias ID
        /// </summary>
        private static readonly Regex ValidationPattern = new(@"^[a-z0-9_-]{1,32}$", RegexOptions.Compiled);

        /// <summary>
        /// Generates a random rich menu alias ID matching the pattern [a-z0-9_-]{1,32}
        /// </summary>
        /// <param name="length">Length of the alias ID (default: 16, must be between 1 and 32)</param>
        /// <returns>A random string matching the rich menu alias ID pattern</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when length is less than 1 or greater than 32</exception>
        public static string Generate(int length = 16)
        {
            if (length < MinLength || length > MaxLength)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(length),
                    length,
                    $"Length must be between {MinLength} and {MaxLength}.");
            }

            var result = new StringBuilder(length);
            using (var rng = RandomNumberGenerator.Create())
            {
                var bytes = new byte[length];
                rng.GetBytes(bytes);

                for (int i = 0; i < length; i++)
                {
                    result.Append(ValidChars[bytes[i] % ValidChars.Length]);
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Validates if a string matches the rich menu alias ID pattern [a-z0-9_-]{1,32}
        /// </summary>
        /// <param name="aliasId">The alias ID to validate</param>
        /// <returns>True if the alias ID is valid, false otherwise</returns>
        public static bool IsValid(string? aliasId)
        {
            if (string.IsNullOrEmpty(aliasId))
            {
                return false;
            }

            if (aliasId.Length < MinLength || aliasId.Length > MaxLength)
            {
                return false;
            }

            return ValidationPattern.IsMatch(aliasId);
        }

        /// <summary>
        /// Validates and throws an exception if the alias ID is invalid
        /// </summary>
        /// <param name="aliasId">The alias ID to validate</param>
        /// <param name="paramName">The parameter name for the exception</param>
        /// <exception cref="ArgumentException">Thrown when the alias ID is invalid</exception>
        public static void ValidateOrThrow(string? aliasId, string paramName = "aliasId")
        {
            if (!IsValid(aliasId))
            {
                throw new ArgumentException(
                    $"Rich menu alias ID must be 1-32 characters and contain only lowercase letters (a-z), numbers (0-9), underscore (_), and hyphen (-). Provided: '{aliasId}'",
                    paramName);
            }
        }
    }
}

