using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RestaurantsWebApi.MySql.Models;
using System.Threading.Tasks;

namespace RestaurantsWebApi.MySql.Filters.ActionFilters
{

    public class ValidateRestaurant_CreateFilter : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments.TryGetValue("restaurant", out var restaurant) && restaurant != null)
            {
                // Your additional validation logic for the restaurant data.
                // For example, check specific properties or conditions.

                // For demonstration purposes, let's check if the restaurant name is not empty.
                if (string.IsNullOrWhiteSpace((restaurant as Restaurant)?.Name))
                {
                    context.Result = new BadRequestObjectResult("Restaurant name is required");
                    return;
                }

                // Check if the Rating is between 0 and 5.
                var rating = (restaurant as Restaurant)?.Rating;
                if (rating < 0 || rating > 5)
                {
                    context.Result = new BadRequestObjectResult("Rating should be between 0 and 5");
                    return;
                }
            }
            else
            {
                context.Result = new BadRequestObjectResult("Invalid or missing restaurant data");
                return;
            }

            await base.OnActionExecutionAsync(context, next);
        }
    }

}
