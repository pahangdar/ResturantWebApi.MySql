using Microsoft.EntityFrameworkCore;
using RestaurantsWebApi.MySql.Models;

namespace RestaurantsWebApi.MySql.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
