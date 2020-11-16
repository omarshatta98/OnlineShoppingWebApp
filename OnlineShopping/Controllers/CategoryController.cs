using OnlineShopping.Data.Repositories;
using OnlineShopping.Models;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace OnlineShopping.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        readonly CategoryRepository categoryRepository;

        public CategoryController()
        {
            categoryRepository = new CategoryRepository();
        }


        [HttpGet]
        public ActionResult Manage()
        {
            return View(categoryRepository.GetAll().ToList());
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
                return RedirectToAction("Manage");
            else
                return View(categoryRepository.GetAll().ToList());
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Category category = categoryRepository.GetById(id);
            if (category == null)
                return HttpNotFound();
            return View(category);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Date = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                categoryRepository.Add(category);
                return RedirectToAction("Index");
            }
            else
                return View(category);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Category category = categoryRepository.GetById(id);
            Session["currentCategory"] = category;
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                Category currentCategory = (Category)Session["currentCategory"];
                category.Date = currentCategory.Date;
                categoryRepository.Update(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Category category = categoryRepository.GetById(id);

            if (category == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            categoryRepository.Delete(category);
            return RedirectToAction("Index");
        }
    }
}