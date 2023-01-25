using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models.Entities;

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

        public async Task<Result<IEnumerable<Category>>> GetAllActiveAsync()
        {
            var categories = await _categoryRepository.GetAllActiveAsync();

            return Result.Ok(categories);
        }

        public async Task<Result<Category>> GetByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return Result.Failure<Category>($"Category with ID {id} does not exist.");
            }

            return Result.Ok(category);
        }

        public async Task<Result<Category>> AddAsync(Category category)
        {
            ArgumentNullException.ThrowIfNull(category, nameof(category));

            var validationResult = await _validator.ValidateAsync(category);
            if (validationResult.IsValid == false)
            {
                return Result.Failure<Category>(validationResult.Errors);
            }

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();

            return Result.Ok(category);
        }

        public async Task<Result<Category>> UpdateAsync(Category category)
        {
            ArgumentNullException.ThrowIfNull(category, nameof(category));

            var validationResult = await _validator.ValidateAsync(category);
            if (validationResult.IsValid == false)
            {
                return Result.Failure<Category>(validationResult.Errors);
            }

            await _categoryRepository.SaveChangesAsync();

            return Result.Ok(category);
        }

        public async Task<Result> DeleteAsync(Category category)
        {
            ArgumentNullException.ThrowIfNull(category, nameof(category));

            _productRepository.DeleteByCategoryId(category.Id);
            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
