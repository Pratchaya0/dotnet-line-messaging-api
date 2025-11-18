using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Response
{
    public class ApiError
    {
        public string Message { get; }
        public IList<ApiErrorDetail>? Details { get; }
        public ApiError(string message, IList<ApiErrorDetail>? details = null)
        {
            Message = message;
            Details = details;
        }
    }
}
