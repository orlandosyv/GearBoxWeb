using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GearBox.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Category Name")]
        [MaxLength(30)]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,100)] // ,ErrorMessage = "---mensaje de error que quiero mostrar-"
        public int DisplayOrder { get; set; }
        public string Description { get; set; }

    }
}
