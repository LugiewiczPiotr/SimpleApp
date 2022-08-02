using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleApp.Core;
using SimpleApp.Core.Interfaces.Logics;
using SimpleApp.Core.Models;
using SimpleApp.WebApi.DTO;

namespace SimpleApp.WebApi.Controllers
{

    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserLogic _userLogic;
        private readonly IMapper _mapper;
        public UserController(IUserLogic userLogic, IMapper mapper)
        {
            _userLogic = userLogic;
            _mapper = mapper;
        }


        /// <summary>
        /// Register user
        /// </summary>
        [HttpPost("Register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(User))]
        public IActionResult Register([FromBody] RegisterDto data)
        {
            if(data == null)
            {
                return BadRequest("Email or password is invalid");
            }
            var user = _mapper.Map<User>(data);
            var createResult = _userLogic.CreateAccount(user);
            if(createResult.Success == false)
            {
                createResult.AddErrorToModelState(ModelState);
                return BadRequest(createResult);
            }

            return StatusCode(201);
        }


        /// <summary>
        /// Login
        /// </summary>
        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public IActionResult Login([FromBody] UserLoginAndPassword data)
        {
            if (data == null)
            {
                return BadRequest("Email or password is invalid");
            }
            var getTokenResult = _userLogic.Authenticate(data);
            if(getTokenResult.Success == false)
            { 
                return Unauthorized(getTokenResult);
            }

            return Ok(Result.Ok(getTokenResult));
        }
    }
}
