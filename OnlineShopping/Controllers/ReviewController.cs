using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;
using OnlineShopping.Models;
using OnlineShopping.Data.Repositories;


namespace OnlineShopping.Web.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        readonly ReviewRepository reviewRepository;
        readonly CustomerRepository customerRepository;

        public ReviewController()
        {
            reviewRepository = new ReviewRepository();
            customerRepository = new CustomerRepository();
        }


        [HttpGet]
        public ActionResult Manage()
        {
            return View(reviewRepository.GetAll().ToList());
        }

        [HttpGet]
        public ActionResult Index()
        {
            var reviews = customerRepository.GetById(User.Identity.GetUserId()).Reviews;

            if (User.IsInRole("Admin"))
                return RedirectToAction("Manage");
            else
                return View(reviews);
        }

        [HttpPost]
        public ActionResult Create(int id, Review review)
        {
            if (ModelState.IsValid)
            {
                review.Date = DateTime.Now;
                review.CustomerId = User.Identity.GetUserId();
                review.ProductId = id;
                reviewRepository.Add(review);
                return RedirectToAction("Details", "Product", new { id = review.ProductId });
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Review review)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Review review = reviewRepository.GetById(id);
            reviewRepository.Delete(review);
            return RedirectToAction("Index");
        }
    }
}