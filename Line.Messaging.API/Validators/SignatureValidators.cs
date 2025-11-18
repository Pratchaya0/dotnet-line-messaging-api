using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Line.Messaging.API.Validates
{
    public class SignatureValidators
    {
        public void XLineSignatureIsValid(string Body, string ChannelSecret, string XLineSignatureHeader)
        {
            if (string.IsNullOrWhiteSpace(XLineSignatureHeader) || string.IsNullOrEmpty(XLineSignatureHeader))
            {
                throw new InvalidDataException("Missing or invalid signature.");
            }

            if (!Convert.TryFromBase64String(XLineSignatureHeader, new Span<byte>(new byte[XLineSignatureHeader.Length]), out int bytesWritten))
            {
                throw new InvalidDataException("The X-Line-Signature is not a valid Base64 string.");
            }

            byte[] decodedSignature = Convert.FromBase64String(XLineSignatureHeader);

            // Create the HMAC-SHA256 hash
            using (HMACSHA256 hmac = new(Encoding.UTF8.GetBytes(ChannelSecret)))
            {
                byte[] digest = hmac.ComputeHash(Encoding.UTF8.GetBytes(Body));

                string computedSignature = Convert.ToBase64String(digest);

                // Compare the hashes
                if (!CryptographicOperations.FixedTimeEquals(decodedSignature, digest))
                {
                    throw new UnauthorizedAccessException("Invalid signature.");
                }
            }
        }
    }
}
