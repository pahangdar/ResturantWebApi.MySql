using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace RestaurantsWebApi.MySql.Filters.ActionFilters
{

    public class ValidateRestaurant_IdFilter : ActionFilterAttribute
    {
        //private readonly AppDbContext _appDbContext;

        //public ValidateRestaurantIdAttribute(AppDbContext appDbContext)
        //{
        //    _appDbContext = appDbContext;
        //}

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ActionArguments.TryGetValue("id", out var idObject) || !(idObject is int id))
            {
                context.Result = new BadRequestObjectResult("Invalid id format");
                return;
            }

            if (id <= 0)
            {
                context.Result = new BadRequestObjectResult("id must be greater than 0");
                return;
            }

            //var restaurant = await _appDbContext.Restaurants.FindAsync(id);

            //if (restaurant == null)
            //{
            //    context.Result = new NotFoundObjectResult($"Restaurant with id {id} not found");
            //    return;
            //}

            await base.OnActionExecutionAsync(context, next);
        }
    }
}
