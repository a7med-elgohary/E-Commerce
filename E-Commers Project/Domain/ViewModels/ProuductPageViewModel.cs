using E_Commers_Project.Domain.Models;

namespace E_Commers_Project.Domain.ViewModels
{
    public class ProuductPageViewModel
    {
        public ProuductPageViewModel()
        {
        }

        public ProuductPageViewModel(int id, string nmae, decimal price, int oldPrice, int stock, Category category, string descrption, List<string> photos, IEnumerable<Product> relatedProuducts)
        {
            this.id = id;
            Nmae = nmae;
            Price = price;
            OldPrice = oldPrice;
            Stock = stock;
            this.category = category;
            Descrption = descrption;
            Photos = photos;
            RelatedProuducts = relatedProuducts;
        }

        public int id { get; set; }
        public string Nmae { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public int Stock { get; set; }
        public Category category { get; set; } = null!;
        public string Descrption { get; set; } = string.Empty;
        public List<string> Photos { get; set; } = new List<string>();
        public IEnumerable<Product>? RelatedProuducts { get; set; }

    }
}
