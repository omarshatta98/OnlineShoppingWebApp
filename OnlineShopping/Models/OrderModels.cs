using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace OnlineShopping.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }
        public string Date { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public int Phone { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
        public double TotalCost { get; set; }
        public Status Status { get; set; }
        public string CustomerId { get; set; }

        public virtual ApplicationUser Customer { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
    public enum Status
    {
        Pending,
        Accept,
        Canceld,
        Done
    }
}