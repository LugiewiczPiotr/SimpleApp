﻿using Microsoft.AspNetCore.Mvc;
using SimpleApp.Core.Models;
using System.Linq;
using System;
using SimpleApp.Web.Models;
using SimpleApp.Core.Interfaces.Logics;
using AutoMapper;

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

            var indexViewModel = _mapper.Map<CategoryViewModel>(categories);
           
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

            var categoryViewModel = new CategoryViewModel()
            {
                Name = getResult.Value.Name
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

            var getResult = _categoryLogic.Add(category);

            if (getResult.Success == false)
            {
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
            var categoryViewModel = new CategoryViewModel()

            {
                Name = getResult.Value.Name
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

            var getResult = _categoryLogic.GetById(categoryViewModel.Id);

            if (getResult.Success == false)
            {
                return NotFound();
            }

            getResult.Value.Name = categoryViewModel.Name;
            var result = _categoryLogic.Update(getResult.Value);

            if(result.Success == false)
            {
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

            var categoryViewModel = new CategoryViewModel
            {
                Name = getResult.Value.Name
            };
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
