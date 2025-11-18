using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line.Messaging.API.Common
{
    public class Result<TSuccess, TError>
    {
        public bool IsSuccess { get; }
        public TSuccess? SuccessValue { get; }
        public TError? ErrorValue { get; }

        private Result(bool isSuccess, TSuccess? successValue, TError? errorValue)
        {
            IsSuccess = isSuccess;
            SuccessValue = successValue;
            ErrorValue = errorValue;
        }

        public static Result<TSuccess, TError> Success(TSuccess value)
            => new(true, value, default);

        public static Result<TSuccess, TError> Failure(TError error)
            => new(false, default, error);
    }

    public class Result<THeader, TSuccess, TError>
    {
        public bool IsSuccess { get; }
        public THeader? HeaderValue { get; }
        public TSuccess? SuccessValue { get; }
        public TError? ErrorValue { get; }

        private Result(bool isSuccess, THeader? headerValue, TSuccess? successValue, TError? errorValue)
        {
            IsSuccess = isSuccess;
            HeaderValue = headerValue;
            SuccessValue = successValue;
            ErrorValue = errorValue;
        }

        public static Result<THeader, TSuccess, TError> Success(THeader header, TSuccess value)
            => new(true, header, value, default);

        public static Result<THeader, TSuccess, TError> Failure(TError error)
            => new(false, default, default, error);
    }
}
