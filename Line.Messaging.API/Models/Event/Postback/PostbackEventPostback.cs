using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.Postback
{
    public class PostbackEventPostback
    {
        public string Data { get; }
        public PostbackEventPostbackParams Params { get; }
        public PostbackEventPostback(string data, PostbackEventPostbackParams @params)
        {
            Data = data;
            Params = @params;
        }

        internal static PostbackEventPostback Create(dynamic postback)
        {
            PostbackEventPostbackParams postbackParams = PostbackEventPostbackParams.Create(postback.@params);
            return new PostbackEventPostback(
                (string)postback.data, postbackParams);
        }
    }
}
