namespace AgriEnergyConnectApp.Models
{
    public class ProductFilterViewModel
    {
        public List<Product>? Products { get; set; }

        // Filters
        public string? Category { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<string>? AvailableCategories { get; set; }

        public string FarmerId { get; set; }
    }
}
