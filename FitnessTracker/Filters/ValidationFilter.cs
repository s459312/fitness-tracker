using FitnessTracker.Contracts.Response.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessTracker.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(x => x.ErrorMessage).ToArray()
                    );
                var errorResponse = new ValidationErrorResponse();

                foreach (var error in errorsInModelState)
                {
                    foreach (var subError in error.Value)
                    {
                        if (errorResponse.Errors.Exists(x => x.FieldName == error.Key))
                        {
                            errorResponse.Errors.Find(x => x.FieldName == error.Key).Errors.Add(subError);
                        }
                        else
                        {
                            var errorModel = new ValidationErrorModel
                            {
                                FieldName = error.Key,
                                Errors = new List<string>(),
                            };
                            errorModel.Errors.Add(subError);
                            errorResponse.Errors.Add(errorModel);
                        }
                    }
                }
                context.Result = new UnprocessableEntityObjectResult(errorResponse);
                return;
            }

            await next();
        }
    }
}