using System.Collections.Generic;


namespace OnlineShopping.Models
{
    public class ShoppingCarts
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public int TotalItem { get; set; }
        public double TotalPrice { get; set; }

        public virtual ApplicationUser Customer { get; set; }
        public virtual ICollection<CartsItems> CartsItems { get; set; }
    }
}