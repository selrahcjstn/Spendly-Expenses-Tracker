using Spendly.Application.Common.Enums;
using Spendly.Application.Dtos.Profile;
using Spendly.Domain.Entities;

namespace Spendly.Application.Common.Result
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public ErrorType? ErrorType { get; }
        public string? ErrorMessage { get; }

        private Result(bool isSuccess, T? value, ErrorType? errorType, string? errorMessage)
        {
            IsSuccess = isSuccess;
            Value = value;
            ErrorType = errorType;
            ErrorMessage = errorMessage;
        }

        public static Result<T> Success(T value) =>
            new(true, value, null, null);

        public static Result<T> Failure(ErrorType errorType, string errorMessage) =>
            new(false, default, errorType, errorMessage);
    }
}
