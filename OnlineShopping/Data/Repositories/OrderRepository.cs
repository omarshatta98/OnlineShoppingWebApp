using OnlineShopping.Models;


namespace OnlineShopping.Data.Repositories
{
    public class OrderRepository : Repository<Order>
    {
        readonly OrderDetailsRepository orderDetailsRepository;
        readonly ProductRepository productRepository;
        readonly ShoppingCartRepository shoppingCartRepository;

        public OrderRepository()
        {
            orderDetailsRepository = new OrderDetailsRepository();
            productRepository = new ProductRepository();
            shoppingCartRepository = new ShoppingCartRepository();
        }

        public void CreateOrder(ShoppingCarts cart, Order order)
        {
            double totalCost = 0;

            foreach (var item in cart.CartsItems)
            {
                Product product = productRepository.GetById(item.ProductId);
                OrderDetails newOrder = new OrderDetails
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    TotalPrice = item.Price * item.Quantity
                };

                orderDetailsRepository.Add(newOrder);
                totalCost += newOrder.TotalPrice;

                product.Quantity -= item.Quantity;
                productRepository.Update(product);
            }

            order.TotalCost = totalCost;
            Update(order);

            shoppingCartRepository.EmptyCart(cart);
        }
    }
}