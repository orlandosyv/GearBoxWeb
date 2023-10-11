using System.ComponentModel.DataAnnotations;

namespace GearBoxWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string DisplayOrder { get; set; }
        public string Description { get; set; }


    }
}
