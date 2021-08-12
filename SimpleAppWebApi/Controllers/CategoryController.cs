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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryLogic _categoryLogic;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryLogic categoryLogic, IMapper mapper)
        {
            _categoryLogic = categoryLogic;
            _mapper = mapper;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public IEnumerable<CategoryDto> Get()
        {
            var categories = _categoryLogic.GetAllActive();
            return _mapper.Map<IList<CategoryDto>>(categories.Value);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
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
            return Ok(getResult); 
        }

        // POST api/<CategoryController>
        [HttpPost]
        public ActionResult Post([FromBody] CategoryDto categoryDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }
            var category = _mapper.Map<Category>(categoryDto);
            var addResult = _categoryLogic.Add(category);
            if (addResult.Success == false)
            {
                addResult.AddErrorToModelState(ModelState);
                return BadRequest();
            }
            return Ok(addResult);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] CategoryDto categoryDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var getResult = _categoryLogic.GetById(categoryDto.Id);

            if (getResult.Success == false)
            {
                getResult.AddErrorToModelState(ModelState);
                return NotFound();
            }

            _mapper.Map(categoryDto, getResult.Value);

            var resultUpdate = _categoryLogic.Update(getResult.Value);

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
            
            return Ok(deleteResult);
        }
    }
}
