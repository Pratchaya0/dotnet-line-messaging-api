using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Common
{
    public enum StatusCodeType
    {
        OK = 200,
        Accepted = 202,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        Conflict = 409,
        Gone = 410,
        PayloadTooLarge = 413,
        UnsupportedMediaType = 415,
        TooManyRequests = 429,
        InternalServerError = 500
    }
}
