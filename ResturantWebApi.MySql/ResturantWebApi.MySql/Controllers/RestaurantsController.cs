using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantsWebApi.MySql.Data;
using RestaurantsWebApi.MySql.Filters.ActionFilters;
using RestaurantsWebApi.MySql.Models;

namespace RestaurantsWebApi.MySql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public RestaurantsController(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var restaurants = await _appDbContext.Restaurants.ToListAsync();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        [ValidateRestaurant_IdFilter]
        public async Task<IActionResult> GetRestaurantById(int id)
        {
            var restaurant = await _appDbContext.Restaurants.FindAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return Ok(restaurant);
        }

        [HttpPost]
        [ValidateRestaurant_CreateFilter]
        [ValidateRestaurant_RatingFilter]
        public async Task<IActionResult> AddRestaurant(Restaurant restaurant)
        {
            _appDbContext.Restaurants.Add(restaurant);
            await _appDbContext.SaveChangesAsync();

            return Ok(restaurant);
        }

        [HttpPut("{id}")]
        [ValidateRestaurant_IdFilter]
        [ValidateRestaurant_UpdateFilter]
        [ValidateRestaurant_RatingFilter]
        public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] Restaurant restaurant)
        {
            var existingRestaurant = await _appDbContext.Restaurants.FindAsync(id);

            if (existingRestaurant == null)
            {
                return NotFound(); // Return 404 Not Found if the restaurant with the given ID is not found.
            }

            // Update the existing restaurant properties with the values from the updatedRestaurant.
            existingRestaurant.Name = restaurant.Name;
            existingRestaurant.Cuisine = restaurant.Cuisine;
            existingRestaurant.Address = restaurant.Address;
            existingRestaurant.PhoneNumber = restaurant.PhoneNumber;
            existingRestaurant.Rating = restaurant.Rating;

            // Update the database.
            _appDbContext.Restaurants.Update(existingRestaurant);
            await _appDbContext.SaveChangesAsync();

            return Ok(existingRestaurant); // Return the updated restaurant with a 200 OK status.
        }

        [HttpDelete("{id}")]
        [ValidateRestaurant_IdFilter]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var existingRestaurant = await _appDbContext.Restaurants.FindAsync(id);

            if (existingRestaurant == null)
            {
                return NotFound(); // Return 404 Not Found if the restaurant with the given ID is not found.
            }

            // Remove the restaurant from the database.
            _appDbContext.Restaurants.Remove(existingRestaurant);
            await _appDbContext.SaveChangesAsync();

            //return NoContent(); // Return 204 No Content on successful deletion.
            return Ok(existingRestaurant);
        }

    }
}
