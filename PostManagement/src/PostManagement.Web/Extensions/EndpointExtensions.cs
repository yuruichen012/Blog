using Ardalis.Result;
using FastEndpoints;

namespace PostManagement.Web.Extensions;

public static class EndpointExtensions
{
    public static bool ReturnValidationErrorsIfInvalid<TRequest, TResult>(this Endpoint<TRequest, Result<TResult>> endpoint) 
        where TRequest : notnull
    {
        if (endpoint.ValidationFailed)
        {
            endpoint.Response = Result.Invalid(endpoint.ValidationFailures.Select(x => new ValidationError(x.PropertyName, x.ErrorMessage, x.ErrorCode, ValidationSeverity.Info)).ToArray());
            return true;
        }

        return false;
    }
}
