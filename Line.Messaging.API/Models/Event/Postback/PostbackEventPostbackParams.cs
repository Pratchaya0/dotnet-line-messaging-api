using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Event.Postback
{
    public class PostbackEventPostbackParams
    {
        public DateOnly? Date { get; }
        public TimeOnly? Time { get; }
        public DateTime? Datetime { get; }
        public string? NewRichMenuAliasId { get; }
        public string? Status { get; }

        public PostbackEventPostbackParams(
            DateOnly? date = null,
            TimeOnly? time = null,
            DateTime? dateTime = null,
            string? newRichMenuAliasId = null,
            string? status = null)
        {
            Date = date;
            Time = time;
            Datetime = dateTime;
            NewRichMenuAliasId = newRichMenuAliasId;
            Status = status;
        }

        internal static PostbackEventPostbackParams Create(dynamic postbackParams)
        {
            DateOnly? date = null;
            if (postbackParams.date != null)
            {
                date = DateOnly.Parse((string)postbackParams.date);
            }
            TimeOnly? time = null;
            if (postbackParams.time != null)
            {
                time = TimeOnly.Parse((string)postbackParams.time);
            }
            DateTime? dateTime = null;
            if (postbackParams.datetime != null)
            {
                dateTime = DateTime.Parse((string)postbackParams.datetime);
            }
            string? newRichMenuAliasId = postbackParams.newRichMenuAliasId != null ? (string)postbackParams.newRichMenuAliasId : null;
            string? status = postbackParams.status != null ? (string)postbackParams.status : null;
            return new PostbackEventPostbackParams(
                date,
                time,
                dateTime,
                newRichMenuAliasId,
                status);
        }
    }
}
