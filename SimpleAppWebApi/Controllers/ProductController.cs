using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleApp.Core;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Models.Entities;
using SimpleApp.WebApi.DTO;

namespace SimpleApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductLogic _productLogic;
        private readonly IMapper _mapper;

        public ProductController(IProductLogic productLogic, IMapper mapper)
        {
            _productLogic = productLogic;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<ProductDto>))]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _productLogic.GetAllActive();
            if (result.Success == false)
            {
                return BadRequest(result);
            }

            var products = _mapper.Map<IList<ProductDto>>(result.Value);
            return Ok(Result.Ok(products));
        }

        /// <summary>
        /// "Get product by id.".
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<ProductDto>))]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var getResult = await _productLogic.GetById(id);
            if (getResult.Success == false)
            {
                return NotFound(getResult);
            }

            var products = _mapper.Map<ProductDto>(getResult.Value);
            return Ok(Result.Ok(products));
        }

        /// <summary>
        /// Create product.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Result<ProductDto>))]
        public async Task<IActionResult> PostAsync([FromBody] ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            var addResult = await _productLogic.Add(product);
            if (addResult.Success == false)
            {
                addResult.AddErrorToModelState(ModelState);
                return BadRequest(addResult);
            }

            var productResult = _mapper.Map<ProductDto>(addResult.Value);
            return CreatedAtAction(
                nameof(GetAsync),
                new { id = addResult.Value.Id },
                Result.Ok(productResult));
        }

        /// <summary>
        /// Update product.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<ProductDto>))]
        public async Task<IActionResult> Put(Guid id, [FromBody] ProductDto productDto)
        {
            var getResult = await _productLogic.GetById(id);
            if (getResult.Success == false)
            {
                getResult.AddErrorToModelState(ModelState);
                return NotFound(getResult);
            }

            _mapper.Map(productDto, getResult.Value);

            var resultUpdate = await _productLogic.Update(getResult.Value);
            if (resultUpdate.Success == false)
            {
                resultUpdate.AddErrorToModelState(ModelState);
                return BadRequest(resultUpdate);
            }

            var productResult = _mapper.Map<ProductDto>(resultUpdate.Value);

            return Ok(Result.Ok(productResult));
        }

        /// <summary>
        /// Delete  product.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var getResult = await _productLogic.GetById(id);
            if (getResult.Success == false)
            {
                return NotFound(getResult);
            }

            var deleteResult = _productLogic.Delete(getResult.Value);
            if (deleteResult.Success == false)
            {
                return BadRequest(deleteResult);
            }

            return NoContent();
        }
    }
}
