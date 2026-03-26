using GlobalBlueHomework.Features.Contracts;
using GlobalBlueHomework.Features.Services;
using GlobalBlueHomework.Features.Validation;
using Microsoft.AspNetCore.Mvc;

namespace GlobalBlueHomework.Features.Endpoints
{
    public static class PurchaseValueEndpoints
    {
        public static void AddPurchaseValueEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/purchaseValues", (
                [FromBody] PurchaseValuesRequest request,
                IPurchaseValuesValidator validator,
                IPurchaseValuesService service) =>
            {
                var errors = validator.Validate(request);

                if (errors.Count > 0)
                {
                    return Results.ValidationProblem(errors);
                }

                var response = service.Calculate(request);

                return Results.Ok(response);
            });
        }
    }
}
