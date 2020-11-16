using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace OnlineShopping.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Display(Name = "Product Image")]
        public string Image { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Product Price")]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Product Quantity")]
        public int Quantity { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Product Description")]
        public string Details { get; set; }

        [Display(Name = "Added Date")]
        public string Date { get; set; }
        public int CategoryId { get; set; }


        public virtual Category Category { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<CartsItems> CartsItems { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}