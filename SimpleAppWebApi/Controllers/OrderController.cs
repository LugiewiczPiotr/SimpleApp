using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleApp.Core;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Models;
using SimpleApp.WebApi.DTO;
using System;
using System.Collections.Generic;

namespace SimpleApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderLogic _orderLogic;
        private readonly IMapper _mapper;
        public OrderController(IOrderLogic orderLogic, IMapper mapper)
        {
            _orderLogic = orderLogic;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all orders.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<OrderDto>))]
        public IActionResult Get()
        {
            var result = _orderLogic.GetAllActive();
            if (result.Success == false)
            {
                return BadRequest(result);
            }
            var orders = _mapper.Map<IList<OrderDto>>(result.Value);
            return Ok(Result.Ok(orders));
        }

        /// <summary>
        /// "Get order by id."
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<OrderDto>))]
        public IActionResult Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var getResult = _orderLogic.GetById(id);
            if (getResult.Success == false)
            {
                return NotFound(getResult);
            }
            var order = _mapper.Map<OrderDto>(getResult.Value);
            return Ok(Result.Ok(order));
        }
        /// <summary>
        /// Create order.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Result<OrderDto>))]
        public IActionResult Post([FromBody] ManageOrderDto manageOrder)
        {
            var order = _mapper.Map<Order>(manageOrder);

            var addResult = _orderLogic.Add(order, User.GetUserId());
            if (addResult.Success == false)
            {
                addResult.AddErrorToModelState(ModelState);
                return BadRequest(addResult);
            }

            var resultDto = _mapper.Map<OrderDto>(addResult.Value);
            return CreatedAtAction(nameof(Get),
                new { id = addResult.Value.Id },
                Result.Ok(resultDto));
        }

        /// <summary>
        /// Update order.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result<OrderDto>))]
        public IActionResult Put(Guid id, [FromBody] OrderDto orderDto)
        {

            var getResult = _orderLogic.GetById(id);

            if (getResult.Success == false)
            {
                getResult.AddErrorToModelState(ModelState);
                return NotFound(getResult);
            }

            _mapper.Map(orderDto, getResult.Value);

            var updateResult = _orderLogic.Update(getResult.Value);

            if (updateResult.Success == false)
            {
                updateResult.AddErrorToModelState(ModelState);
                return BadRequest(updateResult);
            }

            var orderResult = _mapper.Map<OrderDto>(updateResult.Value);
            return Ok(Result.Ok(orderResult));
        }

        /// <summary>
        /// Delete  order.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(Guid id)
        {
            var getResult = _orderLogic.GetById(id);

            if (getResult.Success == false)
            {
                return NotFound(getResult);
            }

            var deleteResult = _orderLogic.Delete(getResult.Value);

            if (deleteResult.Success == false)
            {
                return BadRequest(deleteResult);
            }

            return NoContent();
        }
    }
}