using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Models.Entities;
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


        public async Task<ActionResult> Index()
        {
            var products = await _productLogic.GetAllActive();
            var indexViewModel = new IndexViewModel()
            {
                Products = _mapper.Map<IList<IndexItemViewModel>>(products.Value)
            };

            return View(indexViewModel);
        }

        public async Task<ActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var getResult = await _productLogic.GetById(id);
            if (getResult.Success == false)
            {
                return NotFound();
            }

            var productViewModel = _mapper.Map<ProductViewModel>(getResult.Value);

            return View(productViewModel);
        }

        public async Task<ActionResult> Create()
        {
            var productViewModel = new ProductViewModel();
            await Supply(productViewModel);
            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid == false)
            {
                await Supply(productViewModel);
                return View(productViewModel);
            }

            var product = _mapper.Map<Product>(productViewModel);

            var addProduct = await _productLogic.Add(product);
            if (addProduct.Success == false)
            {
                await Supply(productViewModel);
                addProduct.AddErrorToModelState(ModelState);
                return View(productViewModel);
            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var getResult = await _productLogic.GetById(id);

            if (getResult.Success == false)
            {
                return NotFound();
            }

            var productViewModel = _mapper.Map<ProductViewModel>(getResult.Value);

            await Supply(productViewModel);

            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid == false)
            {
                await Supply(productViewModel);
                return View(productViewModel);
            }

            var getResult = await _productLogic.GetById(productViewModel.Id);
            if (getResult.Success == false)
            {
                return NotFound();
            }

            _mapper.Map(productViewModel, getResult.Value);

            var updateResult = await _productLogic.Update(getResult.Value);
            if (updateResult.Success == false)
            {
                updateResult.AddErrorToModelState(ModelState);
                await Supply(productViewModel);
                return View(productViewModel);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var getResult = await _productLogic.GetById(id);
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
        public async Task<ActionResult> DeletePost(Guid id)
        {
            var getResult = await _productLogic.GetById(id);
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

        private async Task Supply(ProductViewModel viewModel)
        {
            var categoriesList = await _categoryLogic.GetAllActive();

            viewModel.AvailableCategories = _mapper.Map<IEnumerable<SelectItemViewModel>>(categoriesList.Value);
        }
    }
}
