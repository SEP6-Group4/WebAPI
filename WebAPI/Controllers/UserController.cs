﻿using Microsoft.AspNetCore.Mvc;
using WebAPI.Data.User;
using WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("validate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> ValidateUser([FromQuery] string email, [FromQuery] string password)
        {
            if (email == string.Empty || password == string.Empty)
            {
                return BadRequest();
            }

            User user = new User
            {
                Email = email,
                Password = password
            };

            User validatedUser = await userService.ValidateUser(user);

            if (validatedUser == null)
            {
                return Ok(null);
            }
            else
            {
                return Ok(validatedUser);
            }
        }

        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> GetUserByID([FromQuery] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            User user = await userService.GetUserByID(id);

            return Ok(user);
        }        

        [HttpPost("createAccount")]
        public async Task<ActionResult> CreateAccount([FromBody] User user)
        {
            await userService.CreateAccount(user);
            return Ok();
        }

        [HttpPut("updateAccount")]
        public async Task<ActionResult> UpdateAccount([FromBody] User user)
        {
            await userService.UpdateAccountAsync(user);
            return Ok();
        }
    }
}
