using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Common
{
    /// <summary>
    /// LINE Messaging API domain names
    /// https://developers.line.biz/en/reference/messaging-api/#domain-name
    /// </summary>
    public static class DomainName
    {
        /// <summary>
        /// - Get content
        /// - Create audience for uploading user IDs (by file)
        /// - Add user IDs or Identifiers for Advertisers (IFAs) to an audience for uploading user IDs (by file)
        /// - Upload rich menu image
        /// - Download rich menu image
        /// </summary>
        public const string DATA_ENDPOINT = "https://api-data.line.me";

        /// <summary>
        /// Other API endpoints
        /// </summary>
        public const string OTHER_ENDPOINT = "https://api.line.me";
    }
}
