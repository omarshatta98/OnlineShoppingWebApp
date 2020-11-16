using OnlineShopping.Data.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;
using OnlineShopping.Models;


namespace OnlineShopping.Web.Controllers
{
    public class OrderController : Controller
    {
        readonly OrderRepository orderRepository;
        readonly ShoppingCartRepository shoppingCartRepository;
        readonly CustomerRepository customerRepository;

        public OrderController()
        {
            orderRepository = new OrderRepository();
            shoppingCartRepository = new ShoppingCartRepository();
            customerRepository = new CustomerRepository();
        }


        [HttpGet]
        public ActionResult Manage()
        {
            return View(orderRepository.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
                return RedirectToAction("Manage");
            else
            {
                var orders = customerRepository.GetById(User.Identity.GetUserId()).Orders;
                return View(orders);
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Order order = orderRepository.GetById(id);
            return View(order);
        }

        [HttpPost]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                order.CustomerId = User.Identity.GetUserId();
                order.Date = DateTime.Now.ToShortDateString();
                order.Status = Status.Pending;
                orderRepository.Add(order);

                var cart = shoppingCartRepository.GetCart(User.Identity.GetUserId());
                orderRepository.CreateOrder(cart, order);

                return RedirectToAction("/Details/" + order.Id);
            }
            return RedirectToAction("Details", "ShoppingCarts");
        }
    }
}