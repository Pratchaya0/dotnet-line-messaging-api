using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.AccountLink
{
    public class AccountLinkEventLink
    {
        public string Result { get; }
        public string Nonce { get; }
        public AccountLinkEventLink(string result, string nonce)
        {
            Result = result;
            Nonce = nonce;
        }

        internal static AccountLinkEventLink Create(dynamic link)
        {
            return new AccountLinkEventLink(
                (string)link.result,
                (string)link.linkToken);
        }
    }
}
