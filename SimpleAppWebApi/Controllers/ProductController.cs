using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Models;
using SimpleAppWebApi.DTO;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimpleAppWebApi.Controllers
{
    [Route("api/[controller]")]
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
        // GET: api/<CategoryController>
        [HttpGet]
        public IEnumerable<ProductDto> Get()
        {
            var products = _productLogic.GetAllActive();
            return _mapper.Map<IList<ProductDto>>(products.Value);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
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
            return Ok(getResult);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public ActionResult Post([FromBody] ProductDto productDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }
            var product = _mapper.Map<Product>(productDto);
            var addResult = _productLogic.Add(product);
            if (addResult.Success == false)
            {
                addResult.AddErrorToModelState(ModelState);
                return BadRequest();
            }
            return Ok(addResult);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] ProductDto productDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

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
                return BadRequest();
            }
            return Ok(resultUpdate);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
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
                return BadRequest();
            }

            return Ok(deleteResult);
        }
    }
}
