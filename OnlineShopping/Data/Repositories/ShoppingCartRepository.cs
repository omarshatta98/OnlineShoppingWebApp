using OnlineShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace OnlineShopping.Data.Repositories
{
    public class ShoppingCartRepository : Repository<ShoppingCarts>
    {
        readonly CartsItemsRepository cartsItemsRepository = new CartsItemsRepository();
        public ShoppingCarts GetCart(string id)
        {
            ShoppingCarts cart = _set.First(c => c.CustomerId == id);
            cart.TotalItem = GetTotalItem(cart);
            cart.TotalPrice = GetTotalPrice(cart);
            return cart;
        }
        public int GetTotalItem(ShoppingCarts cart)
        {

            int total = cartsItemsRepository.GetAll().Where(c => c.ShoppingCartsId == cart.Id)
                .Select(i => i.Quantity).Sum();

            //int? Count = (from CartsItems in cartsItemsRepository.GetAll()
            //              where CartsItems.ShoppingCartsId == cart.Id
            //              select (int?)CartsItems.Quantity).Sum();

            return total;
        }
        public double GetTotalPrice(ShoppingCarts cart)
        {
            double total = cartsItemsRepository.GetAll().Where(c => c.ShoppingCartsId == cart.Id)
                .Select(i => i.Price * i.Quantity).Sum();

            //double? Total = (from CartsItems in cartsItemsRepository.GetAll()
            //                 where CartsItems.ShoppingCartsId == cart.Id
            //                 select (double?)CartsItems.Price * CartsItems.Quantity).Sum();

            return total;
        }
        public void AddToCart(ShoppingCarts cart, Product product)
        {
            var item = cartsItemsRepository.GetAll()
                .SingleOrDefault(i => i.ShoppingCartsId == cart.Id && i.ProductId == product.Id);

            if (item == null)
            {
                item = new CartsItems
                {
                    Date = DateTime.Now.ToShortDateString(),
                    Quantity = 1,
                    Price = (int)product.Price,
                    SubTotal = product.Price * 1,
                    ProductId = product.Id,
                    ShoppingCartsId = cart.Id
                };
                cartsItemsRepository.Add(item);
            }
            else
            {
                item.Quantity++;
                item.SubTotal += item.Price;
                cartsItemsRepository.Update(item);
            }
        }
        public List<CartsItems> GetItems(ShoppingCarts cart)
        {
            return cart.CartsItems.ToList();
        }
        public void EmptyCart(ShoppingCarts cart)
        {
            var cartItems = cartsItemsRepository.GetAll()
                .Where(c => c.ShoppingCartsId == cart.Id);

            cartsItemsRepository.DeleteAll(cartItems);
        }
    }
}