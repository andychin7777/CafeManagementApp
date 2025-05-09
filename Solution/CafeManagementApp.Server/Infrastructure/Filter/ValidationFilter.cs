using DomainResults.Common;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CafeManagementApp.Server.Infrastructure.Filter
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                // Extract validation errors
                var errors = context.ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .Select(ms => new
                    {
                        Field = ms.Key,
                        Errors = ms.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    });

                // Flatten the nested collections into a single list of strings
                var flattenedErrors = errors
                    .SelectMany(x => x.Errors.Select(y => $"{x.Field} - {y}"))
                    .ToList();

                // Return DomainResult with validation errors
                var domainResult = DomainResult.Failed(flattenedErrors);

                // Set the naming policy to null to default to default casing.
                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = null
                };
                context.Result = new JsonResult(domainResult, jsonOptions)
                {
                    StatusCode = StatusCodes.Status400BadRequest // Set HTTP status code to 400
                };
            }
        }
    }
}
