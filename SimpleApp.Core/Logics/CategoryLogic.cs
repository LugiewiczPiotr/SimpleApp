using System;
using System.Collections.Generic;
using FluentValidation;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models;

namespace SimpleApp.Core.Logics
{
    public class CategoryLogic : ICategoryLogic
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IValidator<Category> _validator;
        public CategoryLogic(
            ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            IValidator<Category> validator)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _validator = validator;
        }

        public Result<IEnumerable<Category>> GetAllActive()
        {
            var categories = _categoryRepository.GetAllActive();

            return Result.Ok(categories);
        }

        public Result<Category> GetById(Guid id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                return Result.Failure<Category>($"Category with ID {id} does not exist.");
            }

            return Result.Ok(category);
        }

        public Result<Category> Add(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            var validationResult = _validator.Validate(category);
            if (validationResult.IsValid == false)
            {
                return Result.Failure<Category>(validationResult.Errors);
            }

            _categoryRepository.Add(category);
            _categoryRepository.SaveChanges();

            return Result.Ok(category);
        }

        public Result<Category> Update(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            var validationResult = _validator.Validate(category);
            if (validationResult.IsValid == false)
            {
                return Result.Failure<Category>(validationResult.Errors);
            }

            _categoryRepository.SaveChanges();

            return Result.Ok(category);
        }

        public Result Delete(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            _productRepository.DeleteByCategoryId(category.Id);
            _categoryRepository.Delete(category);
            _categoryRepository.SaveChanges();

            return Result.Ok();
        }
    }
}
