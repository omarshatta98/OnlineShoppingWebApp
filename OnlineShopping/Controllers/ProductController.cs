using OnlineShopping.Data.Repositories;
using OnlineShopping.Models;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace OnlineShopping.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        readonly ProductRepository productRepository;
        readonly CategoryRepository categoryRepository;

        public ProductController()
        {
            productRepository = new ProductRepository();
            categoryRepository = new CategoryRepository();
        }


        [HttpGet]
        public ActionResult Manage()
        {
            return View(productRepository.GetAll().ToList());
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
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            Product product = productRepository.GetById(id);
            if (product == null)
                return HttpNotFound();
            return View(product);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(categoryRepository.GetAll().ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"), img.FileName);
                img.SaveAs(path);
                product.Image = img.FileName;
                product.Date = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                productRepository.Add(product);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(categoryRepository.GetAll().ToList(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Product product = productRepository.GetById(id);
            if (product == null)
                return HttpNotFound();

            ViewBag.CategoryId = new SelectList(categoryRepository.GetAll().ToList(), "Id", "Name");
            Session["CurrentProduct"] = product;
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                Product CurrentProduct = (Product)Session["CurrentProduct"];
                string oldPath = Path.Combine(Server.MapPath("~/Uploads"), CurrentProduct.Image);
                string newPath = (img == null) ? oldPath : Path.Combine(Server.MapPath("~/Uploads"), img.FileName);
                if (oldPath != newPath)
                {
                    System.IO.File.Delete(oldPath);
                    img.SaveAs(newPath);
                    product.Image = img.FileName;
                }
                else
                {
                    product.Image = CurrentProduct.Image;
                }

                product.Date = CurrentProduct.Date;
                productRepository.Update(product);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(categoryRepository.GetAll().ToList(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Product product = productRepository.GetById(id);
            if (product == null)
                return HttpNotFound();

            string oldPath = Path.Combine(Server.MapPath("~/Uploads"), product.Image);
            System.IO.File.Delete(oldPath);
            productRepository.Delete(product);

            return RedirectToAction("Index");
        }
    }
}