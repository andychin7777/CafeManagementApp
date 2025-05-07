using DomainResults.Common;
using DomainResults.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagementApp.Server.Helper
{
    public static class DomainResultExtensionHelper
    {
        public static IActionResult ToCustomReturnedActionResult(this IDomainResult domainResult,
            ControllerBase controller)
        {
            switch (domainResult.Status)
            {
                case DomainOperationStatus.NotFound:
                    return controller.NotFound(domainResult);
                case DomainOperationStatus.Failed:
                    return controller.BadRequest(domainResult);
                case DomainOperationStatus.Unauthorized:
                    return controller.Forbid();
                case DomainOperationStatus.Conflict:
                    return controller.Conflict(domainResult);
                case DomainOperationStatus.ContentTooLarge:
                    return controller.StatusCode(StatusCodes.Status413PayloadTooLarge, domainResult);
                case DomainOperationStatus.CriticalDependencyError:
                    return controller.StatusCode(StatusCodes.Status503ServiceUnavailable, domainResult);
                default:
                    return domainResult.ToActionResult();
            }
        }
        
        public static IActionResult ToCustomReturnedActionResult<T, T2>(
            this IDomainResult<T> domainResult,
            Func<T, T2> mapFunction,
            ControllerBase controller)
        {
            switch (domainResult.Status)
            {
                case DomainOperationStatus.NotFound:
                    return controller.NotFound(domainResult);
                case DomainOperationStatus.Failed:
                    return controller.BadRequest(domainResult);
                case DomainOperationStatus.Unauthorized:
                    return controller.Forbid();
                case DomainOperationStatus.Conflict:
                    return controller.Conflict(domainResult);
                case DomainOperationStatus.ContentTooLarge:
                    return controller.StatusCode(StatusCodes.Status413PayloadTooLarge, domainResult);
                case DomainOperationStatus.CriticalDependencyError:
                    return controller.StatusCode(StatusCodes.Status503ServiceUnavailable, domainResult);
                default:
                    domainResult.Deconstruct(out var value, out var deconstructDomain);
                    return (mapFunction(value), deconstructDomain).ToActionResult();
            }
        }
    }
}
