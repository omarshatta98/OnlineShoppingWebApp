using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using OnlineShopping.Data.Repositories;
using OnlineShopping.Models;


namespace OnlineShopping.Web.Controllers
{
    [Authorize]
    public class ShoppingCartsController : Controller
    {
        readonly ShoppingCartRepository shoppingCartRepository;
        readonly ProductRepository productRepository;
        readonly CartsItemsRepository cartsItemsRepository;

        public ShoppingCartsController()
        {
            shoppingCartRepository = new ShoppingCartRepository();
            productRepository = new ProductRepository();
            cartsItemsRepository = new CartsItemsRepository();
        }


        [HttpGet]
        public ActionResult Create(string id)
        {
            if (id != null)
            {
                ShoppingCarts cart = new ShoppingCarts
                {
                    CustomerId = id
                };
                shoppingCartRepository.Add(cart);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Index()
        {
            var cart = shoppingCartRepository.GetCart(User.Identity.GetUserId());

            return View(cart);
        }

        [HttpGet]
        public ActionResult Details()
        {
            var cart = shoppingCartRepository.GetCart(User.Identity.GetUserId());
            return View(cart);
        }

        [HttpGet]
        public ActionResult AddToCart(int id)
        {
            var addProduct = productRepository.GetById(id);
            var cart = shoppingCartRepository.GetCart(User.Identity.GetUserId());

            shoppingCartRepository.AddToCart(cart, addProduct);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult RemoveFromCart(int id)
        {
            var cartItem = cartsItemsRepository.GetById(id);
            cartsItemsRepository.Delete(cartItem);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id, int Quantity)
        {
            var item = cartsItemsRepository.GetById(id);
            item.Quantity = Quantity;
            item.SubTotal = item.Quantity * item.Price;
            cartsItemsRepository.Update(item);
            return RedirectToAction("Index");
        }
    }
}