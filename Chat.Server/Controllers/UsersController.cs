using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Chat.Data;

namespace Chat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService) => this.userService = userService;

        [HttpGet]
        public IEnumerable<User> Get() => userService.Get(0, 10);

        [HttpGet("{login}")]
        public ActionResult<User> Get(string login)
        {
            try
            {
                return Ok(userService.Read(login));
            }
            catch (UserNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }

            if (userService.Exists(user.Login))
            {
                return BadRequest($"Login '{user.Login}' is already taken.");
            }

            userService.Create(user);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }

            try
            {
                userService.Update(user);
                return Ok();
            }
            catch (UserNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{login}")]
        public IActionResult Delete(string login)
        {
            try
            {
                userService.Delete(login);
                return Ok();
            }
            catch (UserNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }
    }
}
