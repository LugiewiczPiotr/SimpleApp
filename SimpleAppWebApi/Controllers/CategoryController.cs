﻿using AutoMapper;
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

        /// <summary>
        /// Get all category.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<IEnumerable<CategoryDto>>))]
        public ActionResult<IEnumerable<CategoryDto>> Get()
        {
            var result = _categoryLogic.GetAllActive();
            if (result.Success == false)
            {
                return BadRequest(result);
            }
            var categories = _mapper.Map<IList<CategoryDto>>(result.Value);
            return Ok(Result.Ok(categories));
        }

        /// <summary>
        /// "Get product by id."
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<IEnumerable<CategoryDto>>))]
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
            var category = _mapper.Map<CategoryDto>(getResult.Value);
            return Ok(Result.Ok(category));
        }
        /// <summary>
        /// Create category.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Result<IEnumerable<CategoryDto>>))]
        public ActionResult Post([FromBody] CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            var addResult = _categoryLogic.Add(category);
            if (addResult.Success == false)
            {
                addResult.AddErrorToModelState(ModelState);
                return BadRequest(addResult);
            }
            var categoryResult = _mapper.Map<CategoryDto>(addResult.Value);
            return CreatedAtAction(nameof(Get),
                new { id = addResult.Value.Id },
                Result.Ok(categoryResult));
        }

        /// <summary>
        /// Update category.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Result<IEnumerable<CategoryDto>>))]
        public ActionResult Put(Guid id, [FromBody] CategoryDto categoryDto)
        {

            var getResult = _categoryLogic.GetById(id);

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
                return BadRequest(resultUpdate);
            }
            return Ok(Result.Ok(resultUpdate));
        }

        /// <summary>
        /// Delete  category.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<IEnumerable<CategoryDto>>))]
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
                return BadRequest(deleteResult);
            }

            return Ok(Result.Ok(deleteResult));
        }
    }
}
