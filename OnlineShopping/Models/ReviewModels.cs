using System;
using System.ComponentModel.DataAnnotations;


namespace OnlineShopping.Models
{
    public class Review
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Added Date")]
        public DateTime Date { get; set; }

        [Required]
        public double Rate { get; set; }

        [Required]
        public string Comment { get; set; }

        [Display(Name = "WHAT'S GOOD ABOUT THIS PRODUCT:")]
        public string Pros { get; set; }

        [Display(Name = "WHAT'S NOT SO GOOD ABOUT THIS PRODUCT:")]
        public string Cons { get; set; }
        public string CustomerId { get; set; }
        public int ProductId { get; set; }

        public virtual ApplicationUser Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}