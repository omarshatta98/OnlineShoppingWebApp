using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace OnlineShopping.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [Display(Name = "Added Date")]
        public string Date { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}