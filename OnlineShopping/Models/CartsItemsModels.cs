using System.ComponentModel.DataAnnotations;


namespace OnlineShopping.Models
{
    public class CartsItems
    {
        public int Id { get; set; }
        public int ShoppingCartsId { get; set; }
        public int ProductId { get; set; }

        [Display(Name = "Added Date")]
        public string Date { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public double SubTotal { get; set; }


        public virtual ShoppingCarts ShoppingCart { get; set; }
        public virtual Product Product { get; set; }
    }
}