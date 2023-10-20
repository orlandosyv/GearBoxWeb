using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace GearBox.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Display(Name ="List Price")]
        [Range(0,10000)]
        public double ListPrice { get; set; }
        [Required]
        [Display(Name = "Price for 1-50")]
        [Range(0, 10000)]
        public double Price { get; set; }
        [Required]
        [Display(Name = "Price for +50")]
        [Range(0, 10000)]
        public double Price50 { get; set; }
        [Required]
        [Display(Name = "Price for +100")]
        [Range(0, 10000)]
        public double Price100 { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]        
        public Category Category { get; set; }

        // Own Properties for GearBox

        [Display(Name = "Product Discount")]
        [Range(0, 0.99)]
        public double ProductDiscount { get; set; }

        [Display(Name = "Cost of Product")]
        [Range(0, 10000)]
        public double CostOfProduct { get; set; }
                   
        [Range(0, 10000)]
        public double Weight { get; set; }

        [Range(0, 10000)]
        public double Large{ get; set; }

        [Range(0, 10000)]
        public double Width { get; set; }

        [Range(0, 10000)]
        public double Height { get; set; }
        
    }
}
