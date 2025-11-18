using System;
using System.Security.Cryptography;
using System.Text;

namespace Line.Messaging.API.Helpers
{
    /// <summary>
    /// Helper class for generating resume request keys
    /// </summary>
    public static class ResumeRequestKeyHelper
    {
        /// <summary>
        /// Valid characters for resume request key: 0-9, a-z, A-Z, -, _
        /// </summary>
        private const string ValidChars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-_";

        /// <summary>
        /// Generates a random resume request key matching the pattern [0-9a-zA-Z\-_]{1,100}
        /// </summary>
        /// <param name="length">Length of the key (default: 32, must be between 1 and 100)</param>
        /// <returns>A random string matching the resume request key pattern</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when length is less than 1 or greater than 100</exception>
        public static string Generate(int length = 32)
        {
            if (length < 1 || length > 100)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(length),
                    length,
                    "Length must be between 1 and 100.");
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
        /// Validates if a string matches the resume request key pattern [0-9a-zA-Z\-_]{1,100}
        /// </summary>
        /// <param name="key">The key to validate</param>
        /// <returns>True if the key is valid, false otherwise</returns>
        public static bool IsValid(string? key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }

            if (key.Length < 1 || key.Length > 100)
            {
                return false;
            }

            foreach (char c in key)
            {
                if (!ValidChars.Contains(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
}

