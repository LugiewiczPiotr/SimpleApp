using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Interfaces.Repositories;
using SimpleApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleApp.Core.Logics
{
    public class ProductLogic : IProductLogic
    {
        private readonly IProductRepository _productRepository;

        public ProductLogic(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Result<IEnumerable<Product>> GetAllActive()
        {
            var products = _productRepository.GetAllActive();

            return Result.Ok(products);
        }

        public Result<Product> GetById(Guid id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return Result.Failure<Product>($"Product with ID {id} does not exist.");
            }

            return Result.Ok(product);
        }

        public Result<Product> Add(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _productRepository.Add(product);
            _productRepository.SaveChanges();

            return Result.Ok(product);
        }

        public Result<Product> Update(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _productRepository.SaveChanges();

            return Result.Ok(product);
        }

        public Result Delete(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _productRepository.Delete(product);
            _productRepository.SaveChanges();

            return Result.Ok();
        }
        }
    }
