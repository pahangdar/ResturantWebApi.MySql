namespace RestaurantsWebApi.MySql.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cuisine { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string? PhoneNumber { get; set; }
        public double Rating { get; set; }
    }
}
