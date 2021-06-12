using Microsoft.AspNetCore.Mvc;
using SimpleApp.Infrastructure.Data;
using SimpleApp.Core.Models;
using System.Linq;
using System;
using SimpleApp.Web.Models;

namespace SimpleApp.Web.Controllers
{
    public class CategoryController : Controller
    {
        // GET: CategoryController
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var categories = _context.Categories.ToList();
            var indexViewModel = new IndexViewModel()
            {
                CategoriesViewModels = categories.Select(x => new CategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name

                }).ToList()
            };
            return View(indexViewModel) ;
        }

        // GET: CategoryController/Details/5
        public IActionResult Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var result = _context.Categories.FirstOrDefault(x => x.Id == id);
            if(result == null)
            {
                return NotFound();
            }
            var categoryViewModel = new CategoryViewModel()
            {
                Name = result.Name
            };

            return View(categoryViewModel);
        }
        // GET: CategoryController/Create
        public ActionResult Create()
        {
            var categoryViewModel = new CategoryViewModel();
            return View(categoryViewModel);
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid == false)
            {
                return View(categoryViewModel);
            }
            var category = new Category()
            {
                Name = categoryViewModel.Name
            };
           
            _context.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index"); 
        }
        // GET: CategoryController/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var result = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            var categoryViewModel = new CategoryViewModel()
            {
                Name = result.Name
            };
            return View(categoryViewModel);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid == false)
            {
                return View(categoryViewModel);
            }
            var category = new Category()
            {
                Id = categoryViewModel.Id,
                Name = categoryViewModel.Name
            };

            _context.Update(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: CategoryController/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var result = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            var categoryViewModel = new CategoryViewModel
            {
                Name = result.Name
            };
            return View(categoryViewModel);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, CategoryViewModel categoryViewModel)
        {
            
            var category = new Category
            {
                Name = categoryViewModel.Name,
                Id = categoryViewModel.Id
            };
            var result = _context.Categories.Find(id);
            _context.Categories.Remove(result);
            _context.SaveChanges();
            return RedirectToAction("Index");



        }
    }
}
