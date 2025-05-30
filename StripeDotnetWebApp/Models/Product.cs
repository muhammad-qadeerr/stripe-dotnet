namespace StripeDotnetWebApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string PriceId { get; set; } = null!;
    }
}
