using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models.Entities;

namespace SimpleApp.Core.Logics
{
    public class ProductLogic : IProductLogic
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidator<Product> _validator;

        public ProductLogic(IProductRepository productRepository, IValidator<Product> validator)
        {
            _productRepository = productRepository;
            _validator = validator;
        }

        public async Task<Result<IEnumerable<Product>>> GetAllActiveAsync()
        {
            var products = await _productRepository.GetAllActiveAsync();

            return Result.Ok(products);
        }

        public async Task<Result<Product>> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return Result.Failure<Product>($"Product with ID {id} does not exist.");
            }

            return Result.Ok(product);
        }

        public async Task<Result<Product>> AddAsync(Product product)
        {
            ArgumentNullException.ThrowIfNull(product, nameof(product));

            var validationResult = await _validator.ValidateAsync(product);
            if (validationResult.IsValid == false)
            {
                return Result.Failure<Product>(validationResult.Errors);
            }

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            return Result.Ok(product);
        }

        public async Task<Result<Product>> UpdateAsync(Product product)
        {
            ArgumentNullException.ThrowIfNull(product, nameof(product));

            var validationResult = await _validator.ValidateAsync(product);
            if (validationResult.IsValid == false)
            {
                return Result.Failure<Product>(validationResult.Errors);
            }

            await _productRepository.SaveChangesAsync();

            return Result.Ok(product);
        }

        public Result Delete(Product product)
        {
            ArgumentNullException.ThrowIfNull(product, nameof(product));

            _productRepository.Delete(product);
            _productRepository.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
