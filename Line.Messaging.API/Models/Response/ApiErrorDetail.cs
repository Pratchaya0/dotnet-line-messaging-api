using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Models.Response
{
    public class ApiErrorDetail
    {
        public string? Message { get; }
        public string? Property { get; }
        public ApiErrorDetail(string? message = null, string? property = null)
        {
            Message = message;
            Property = property;
        }
    }
}
