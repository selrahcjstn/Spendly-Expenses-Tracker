namespace Spendly.Application.Common.Enums
{
    public enum ErrorType
    {
        None = 0,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        Conflict = 409,
        InternalError = 500,
    }
}
