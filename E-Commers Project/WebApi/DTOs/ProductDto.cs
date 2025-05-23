namespace E_Commers_Project.WebApi.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreateAt { get; set; }
        public bool IsFavorite { get; set; }
        public int? CategoryId { get; set; }
        public int? SaleId { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
