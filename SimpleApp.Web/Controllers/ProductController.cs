using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Models;
using SimpleApp.Web.ViewModels;
using SimpleApp.Web.ViewModels.Products;

namespace SimpleApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductLogic _productLogic;
        private readonly ICategoryLogic _categoryLogic;
        private readonly IMapper _mapper;

        public ProductController(IProductLogic productLogic, ICategoryLogic categoryLogic, IMapper mapper)
        {
            _productLogic = productLogic;
            _categoryLogic = categoryLogic;
            _mapper = mapper;
        }

        // GET: ProductController1
        public ActionResult Index()
        {
            var products = _productLogic.GetAllActive();
            var indexViewModel = new IndexViewModel()
            {
                Products = _mapper.Map<IList<IndexItemViewModel>>(products.Value)
            };

            return View(indexViewModel);
        }

        public ActionResult Details(Guid id)
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

            var productViewModel = _mapper.Map<ProductViewModel>(getResult.Value);

            return View(productViewModel);
        }

        public ActionResult Create()
        {
            var productViewModel = new ProductViewModel();
            Supply(productViewModel);
            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid == false)
            {
                Supply(productViewModel);
                return View(productViewModel);
            }

            var product = _mapper.Map<Product>(productViewModel);

            var addProduct = _productLogic.Add(product);
            if (addProduct.Success == false)
            {
                Supply(productViewModel);
                addProduct.AddErrorToModelState(ModelState);
                return View(productViewModel);
            }

            return RedirectToAction("Index");
        }

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

            var productViewModel = _mapper.Map<ProductViewModel>(getResult.Value);

            Supply(productViewModel);

            return View(productViewModel);
        }

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

            _mapper.Map(productViewModel, getResult.Value);

            var updateResult = _productLogic.Update(getResult.Value);
            if (updateResult.Success == false)
            {
                updateResult.AddErrorToModelState(ModelState);
                Supply(productViewModel);
                return View(productViewModel);
            }

            return RedirectToAction("Index");
        }

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

            var productViewModel = _mapper.Map<ProductViewModel>(getResult.Value);
            return View(productViewModel);
        }

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

            viewModel.AvailableCategories = _mapper.Map<IEnumerable<SelectItemViewModel>>(categoriesList.Value);
        }
    }
}
