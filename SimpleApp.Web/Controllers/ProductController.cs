using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleApp.Core.Models;
using SimpleApp.Infrastructure.Data;
using SimpleApp.Web.Models;
using System;
using System.Linq;

namespace SimpleApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        // GET: ProductController1
        public ActionResult Index()
        {
            var products = _context.Products;
            var indexViewModel = new IndexViewModel
            {
                ProductsViewModels = products.Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Description = x.Description,

                }).ToList()
            };
            return View(indexViewModel);
        }

        // GET: ProductController1/Details/5
        public ActionResult Details(Guid id)
        {
            if(id == Guid.Empty)
            {
                return NotFound();
            }
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if(product == null)
            {
                return (NotFound());
            }
            var producktViewModel = new ProductViewModel
            {
                Id = product.Id,
                Description = product.Description,
                Name = product.Name,
                Price = product.Price
            };
             
            return View(producktViewModel);
        }

        // GET: ProductController1/Create
        public ActionResult Create()
        {
            var productViewModel = new ProductViewModel();
            return View(productViewModel);
        }

        // POST: ProductController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid == false)
            {
                return View(productViewModel);
            }
            var product = new Product
            {
                Name = productViewModel.Name,
                Description = productViewModel.Description,
                Price = productViewModel.Price
            };
            _context.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: ProductController1/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var result = _context.Products.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            var productViewModel = new ProductViewModel()
            {
                Name = result.Name
            };
            return View(productViewModel);
        }

        // POST: ProductController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel productViewModel)
        {

            if (ModelState.IsValid == false)
            {
                return View(productViewModel);
            }

            var product = _context.Categories.FirstOrDefault(x => x.Id == productViewModel.Id);
            if (product == null)
            {
                return NotFound();
            }
           product.Name = productViewModel.Name;
            _context.Update(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: ProductController1/Delete/5
        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return (NotFound());
            }
            var productViewModel = new ProductViewModel
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
            return View(productViewModel);
        }

        // POST: ProductController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeletePost(Guid id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
