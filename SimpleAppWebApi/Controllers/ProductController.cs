using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleApp.Core;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Models;
using SimpleApp.WebApi.DTO;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public ActionResult<IEnumerable<ProductDto>> Get()
        {
            var result = _productLogic.GetAllActive();
            if(result.Success == false)
            {
                return BadRequest(result);
            }
            var products =_mapper.Map<IList<ProductDto>>(result.Value);
            return Ok(Result.Ok(products)); 
        }

        /// <summary>
        /// "Get product by id."
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<ProductDto>))]
        public ActionResult Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var getResult = _productLogic.GetById(id);
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
        public ActionResult Post([FromBody] ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var addResult = _productLogic.Add(product);
            if (addResult.Success == false)
            {
                addResult.AddErrorToModelState(ModelState);
                return BadRequest(addResult);
            }
            var productResult = _mapper.Map<ProductDto>(addResult.Value);
            return CreatedAtAction(nameof(Get),
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
        public ActionResult Put(Guid id, [FromBody] ProductDto productDto)
        {
            var getResult = _productLogic.GetById(id);

            if (getResult.Success == false)
            {
                getResult.AddErrorToModelState(ModelState);
                return NotFound(getResult);
            }

            _mapper.Map(productDto, getResult.Value);

            var resultUpdate = _productLogic.Update(getResult.Value);

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<ProductDto>))]
        public ActionResult Delete(Guid id)
        {
            var getResult = _productLogic.GetById(id);

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
