using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RestaurantsWebApi.MySql.Models;
using System.Threading.Tasks;

namespace RestaurantsWebApi.MySql.Filters.ActionFilters
{
    public class ValidateRestaurant_UpdateFilter : ActionFilterAttribute
    {
 
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            if (context.ActionArguments.TryGetValue("id", out var idObject) && context.ActionArguments.TryGetValue("restaurant", out var restaurant))
            {
                if (!(idObject is int id) || id <= 0)
                {
                    context.Result = new BadRequestObjectResult("Invalid or missing restaurant id");
                    return;
                }

                // For demonstration purposes, let's check if the restaurant name is not empty.
                if (string.IsNullOrWhiteSpace((restaurant as Restaurant)?.Name))
                {
                    context.Result = new BadRequestObjectResult("Restaurant name is required");
                    return;
                }

                // Check if the provided ID matches the ID in the restaurant data
                if (id != (restaurant as Restaurant)?.Id)
                {
                    context.Result = new BadRequestObjectResult("Mismatch between provided ID and restaurant ID");
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
                context.Result = new BadRequestObjectResult("Invalid or missing data for update");
                return;
            }

            await base.OnActionExecutionAsync(context, next);
        }
    }

}
