using Microsoft.AspNetCore.Mvc;
using SimpleApp.Core.Models;
using System;
using SimpleApp.Core.Interfaces.Logics;
using AutoMapper;
using SimpleApp.Web.ViewModels.Categories;
using System.Collections.Generic;

namespace SimpleApp.Web.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ICategoryLogic _categoryLogic;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryLogic categoryLogic, IMapper mapper)
        {
            _categoryLogic = categoryLogic;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            var categories = _categoryLogic.GetAllActive();

            var indexViewModel = new IndexViewModel()
            {
                Categories = _mapper.Map<IList<IndexItemViewModel>>(categories.Value)
            };
            


            return View(indexViewModel);
        }

        // GET: CategoryController/Details/5
        public IActionResult Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var getResult = _categoryLogic.GetById(id);

            if (getResult.Success == false)
            {
                return NotFound();
            }

            var categoryViewModel = _mapper.Map<CategoryViewModel>(getResult.Value);

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

            var category = _mapper.Map<Category>(categoryViewModel);

            var addResult = _categoryLogic.Add(category);

            if (addResult.Success == false)
            {
                addResult.AddErrorToModelState(ModelState);
                return View(categoryViewModel);
            }
            return RedirectToAction("Index");
        }
        // GET: CategoryController/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var getResult = _categoryLogic.GetById(id);

            if (getResult.Success == false)
            {
                
                return NotFound();
            }
            var categoryViewModel = _mapper.Map<CategoryViewModel>(getResult.Value);
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

            var getResult = _categoryLogic.GetById(categoryViewModel.Id);

            if (getResult.Success == false)
            {
                getResult.AddErrorToModelState(ModelState);
                return NotFound();
            }

            _mapper.Map(categoryViewModel, getResult.Value);
           
            var result = _categoryLogic.Update(getResult.Value);

            if(result.Success == false)
            {
                result.AddErrorToModelState(ModelState);
                return View(categoryViewModel);
            }
            return RedirectToAction("Index");
        }
        // GET: CategoryController/Delete/5
        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var getResult = _categoryLogic.GetById(id);

            if (getResult.Success == false)
            {
                return NotFound();
            }
            var categoryViewModel = _mapper.Map<CategoryViewModel>(getResult.Value);
            return View(categoryViewModel);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeletePost(Guid id)
        {
            var getResult = _categoryLogic.GetById(id);

            if (getResult.Success == false)
            {
                return NotFound();
            }

            var deleteResult = _categoryLogic.Delete(getResult.Value);

            if (deleteResult.Success == false)
            {
                return BadRequest();
            }
            return RedirectToAction("Index");

        }
    }
}
