using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Models;
using SimpleApp.WebApi.DTO;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        public ActionResult<IEnumerable<ProductDto>> Get()
        {
            var result = _productLogic.GetAllActive();
            if(result.Success == false)
            {
                return NotFound();
            }
            var products =_mapper.Map<IList<ProductDto>>(result.Value);
            return Ok(products);
        }

        /// <summary>
        /// Get id product.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        public ActionResult Get(Guid id)
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
            var product = _mapper.Map<ProductDto>(getResult.Value);
            return Ok(product);
        }

        /// <summary>
        /// Create product.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Product))]
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
                addResult); 
        }

        /// <summary>
        /// Update product.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        public ActionResult Put(Guid id, [FromBody] ProductDto productDto)
        {
            var getResult = _productLogic.GetById(id);

            if (getResult.Success == false)
            {
                getResult.AddErrorToModelState(ModelState);
                return NotFound();
            }

            _mapper.Map(productDto, getResult.Value);

            var resultUpdate = _productLogic.Update(getResult.Value);

            if (resultUpdate.Success == false)
            {
                resultUpdate.AddErrorToModelState(ModelState);
                return BadRequest(resultUpdate);
            }
            return Ok(resultUpdate);
        }

        /// <summary>
        /// Delete  product.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        public ActionResult Delete(Guid id)
        {
            var getResult = _productLogic.GetById(id);

            if (getResult.Success == false)
            {
                return NotFound();
            }

            var deleteResult = _productLogic.Delete(getResult.Value);

            if (deleteResult.Success == false)
            {
                return BadRequest(deleteResult);
            }

            return Ok(deleteResult);
        }
    }
}
