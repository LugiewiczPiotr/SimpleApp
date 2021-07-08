﻿using Microsoft.AspNetCore.Mvc;
using SimpleApp.Core.Models;
using System;
using System.Linq;
using SimpleApp.Web.Models;
using SimpleApp.Web.ViewModels;
using SimpleApp.Core.Interfaces.Logics;

namespace SimpleApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductLogic _productLogic;
        private readonly ICategoryLogic _categoryLogic;
      
        public ProductController(IProductLogic productLogic, ICategoryLogic categoryLogic)
        {
            _productLogic = productLogic;
            _categoryLogic = categoryLogic;

        }
        // GET: ProductController1
        public ActionResult Index()
        {
            var products = _productLogic.GetAllActive();
            var indexViewModel = new ViewModels.Product.IndexViewModel()
            {
                ProductsViewModels = products.Value.Select(x => new ProductViewModel
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
            var getResult = _productLogic.GetById(id);
            if(getResult.Success == false)
            {
                return NotFound();
            }
            var productViewModel = new ProductViewModel
            {
                Id = getResult.Value.Id,
                Description = getResult.Value.Description,
                Name = getResult.Value.Name,
                Price = getResult.Value.Price
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
            var getResultCategory = _categoryLogic.GetById(productViewModel.SelectedCategory);
            if (getResultCategory.Success == false)
            {
                return NotFound();
            }


            var product = new Product
            {
                Name = productViewModel.Name,
                Description = productViewModel.Description,
                Price = productViewModel.Price,
                CategoryId = getResultCategory.Value.Id

            };

            var addProduct =_productLogic.Add(product);
            if (addProduct.Success == false)
            {
                return View(productViewModel);
            }

            return RedirectToAction("Index");
        }

        // GET: ProductController1/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var getResult = _productLogic.GetById(id);
            
            if (getResult.Success == false)
            {
                return NotFound();
            }
            var productViewModel = new ProductViewModel()
            {
                Name = getResult.Value.Name,
                Description = getResult.Value.Description,
                Price = getResult.Value.Price,
                SelectedCategory = getResult.Value.CategoryId

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

            var getResult = _productLogic.GetById(productViewModel.Id);
            if (getResult.Success == false)
            {
                return NotFound();
            }
            var getResultCategory = _categoryLogic.GetById(productViewModel.SelectedCategory);
            if (getResultCategory.Success == false)
            {
                return NotFound();
            }

            getResult.Value.Name = productViewModel.Name;
            getResult.Value.Description = productViewModel.Description;
            getResult.Value.Price = productViewModel.Price;
            getResult.Value.CategoryId = getResultCategory.Value.Id;


            var updateResult = _productLogic.Update(getResult.Value);
            if(updateResult.Success == false)
            {
                return BadRequest();
            }
            
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

            var getResult = _productLogic.GetById(id);
            if (getResult.Success == false)
            {
                return NotFound();
            }
            var productViewModel = new ProductViewModel
            {
                Name = getResult.Value.Name,
                Description = getResult.Value.Description,
                Price = getResult.Value.Price
            };
            return View(productViewModel);
        }

        // POST: ProductController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeletePost(Guid id)
        {
            var getResult = _productLogic.GetById(id);
            if (getResult.Success == false)
            {
                return NotFound();
            }

            var deleteResult = _productLogic.Delete(getResult.Value);

            if (deleteResult.Success == false)
            {
                return BadRequest();
            }

            return RedirectToAction("Index");

            
        }

        private void Supply(ProductViewModel viewModel)
        {
            var categoriesList = _categoryLogic.GetAllActive();
            viewModel.AvailableCategories = categoriesList.Value.Select(x => new SelectItemViewModel()
            {
                Value = x.Id.ToString(),
                Display = x.Name

            });
        }
    }
}
