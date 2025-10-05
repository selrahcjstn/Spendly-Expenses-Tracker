using Microsoft.AspNetCore.Mvc;

namespace Spendly.Api.Extensions
{
    public static class ProblemDetail
    {

        public static IActionResult ToProblem(
            this ControllerBase controller,
            string title,
            int statusCode,
            string? message,
            string? type = null
            )
        {
            return controller.Problem(
            type: type ?? $"https://api.spendly.com/errors/{title.ToLower()}",
            title: title,
            statusCode: statusCode,
            detail: message
        );
        }

       public static IActionResult InvalidInput(
           this ControllerBase controller,
           string[]? message,
           string? type = null
       )
            {
                var problemDetails = new ProblemDetails
                {
                    Type = type ?? "https://api.spendly.com/errors/validationerror",
                    Title = "ValidationError",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "One or more validation errors occurred."
                };

                problemDetails.Extensions["errors"] = message;

                return controller.StatusCode(StatusCodes.Status400BadRequest, problemDetails);
            }

    }
}