using Microsoft.AspNetCore.Mvc;
using SimpleApp.Core.Models;
using SimpleApp.Infrastructure.Data;
using System;
using System.Linq;
using SimpleApp.Web.Models;
using SimpleApp.Web.ViewModels;

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
            var indexViewModel = new ViewModels.Product.IndexViewModel()
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
                return NotFound();
            }
            var productViewModel = new ProductViewModel
            {
                Id = product.Id,
                Description = product.Description,
                Name = product.Name,
                Price = product.Price
            };
             
            return View(productViewModel);
        }

        // GET: ProductController1/Create
        public ActionResult Create()
        {
            var productViewModel = new ProductViewModel();
            Supply(productViewModel);
            return View(productViewModel);
        }

        // POST: ProductController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid == false)
            {
                Supply(productViewModel);
                return View(productViewModel);
            }

            var selectedCategory = _context.Categories
              .FirstOrDefault(x => x.Id == productViewModel.SelectedCategory);

            var product = new Product
            {
                Name = productViewModel.Name,
                Description = productViewModel.Description,
                Price = productViewModel.Price,
                Category = selectedCategory
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
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            var productViewModel = new ProductViewModel()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                SelectedCategory = product.CategoryId

            };

            Supply(productViewModel);

            return View(productViewModel);
        }

        // POST: ProductController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel productViewModel)
        {

            if (ModelState.IsValid == false)
            {
                Supply(productViewModel);
                return View(productViewModel);
            }

            var product = _context.Products.FirstOrDefault(x => x.Id == productViewModel.Id);
            if (product == null)
            {
                return NotFound();
            }
            product.Name = productViewModel.Name;
            product.Description = productViewModel.Description;
            product.Price = productViewModel.Price;
            product.Category = _context.Categories.FirstOrDefault(x => x.Id == productViewModel.SelectedCategory);


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

        private void Supply(ProductViewModel viewModel)
        {
            var categories = _context.Categories.ToList();
            viewModel.AvailableCategories = categories.Select(x => new SelectItemViewModel()
            {
                Value = x.Id.ToString(),
                Display = x.Name

            });
        }
    }
}
