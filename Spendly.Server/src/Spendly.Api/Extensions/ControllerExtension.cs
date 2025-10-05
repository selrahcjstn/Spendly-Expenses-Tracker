using Microsoft.AspNetCore.Mvc;
using Spendly.Application.Common.Enums;
using Spendly.Application.Common.Result;
using System.Net;

namespace Spendly.Api.Extensions
{
    public static class ControllerExtension
    {
        public static IActionResult ToActionResult<T>(this 
            ControllerBase controller, 
            Result<T> result,
            string? createdAtActionName = null,
            object? routeValues = null
            )
        {
            if (result.IsSuccess)
            {
                if (createdAtActionName != null)
                {
                    return controller.CreatedAtAction(
                        createdAtActionName,
                        routeValues,
                        result.Value
                    );
                }
               return controller.Ok(result.Value);
      
            }

            var statusCode = result.ErrorType switch
            {
                ErrorType.NotFound => HttpStatusCode.NotFound,
                ErrorType.Conflict => HttpStatusCode.Conflict,
                ErrorType.Unauthorized => HttpStatusCode.Unauthorized,
                ErrorType.BadRequest => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            return controller.ToProblem(
                title: statusCode.ToString(),
                statusCode: (int)statusCode,
                message: result.ErrorMessage
            );


        }
    }
}
