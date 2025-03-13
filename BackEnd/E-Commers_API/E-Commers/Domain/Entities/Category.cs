using System.ComponentModel.DataAnnotations;

namespace E_Commers.Domain.Entities
{
    public class Category
    {
        //Attributes
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
