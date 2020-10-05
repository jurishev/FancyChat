using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Chat.Data;

namespace Chat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;

        public AuthController(IUserService userService) => this.userService = userService;

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]User user)
        {
            if (user is null)
            {
                return BadRequest();
            }

            try
            {
                if (user.Password != userService.Read(user.Login).Password)
                {
                    return Unauthorized();
                }
            }
            catch (UserNotFoundException)
            {
                return NotFound();
            }

            return Ok(new { token = GetJwt(user.Login) });
        }

        [HttpGet, Route("user"), Authorize]
        public ActionResult<User> GetUser()
        {
            var claim = User.Claims.Where(claim => claim.Type == "Login").FirstOrDefault();

            if (claim is null)
            {
                return Unauthorized();
            }

            try
            {
                return Ok(userService.Read(claim.Value));
            }
            catch (UserNotFoundException)
            {
                return NotFound();
            }
        }

        private string GetJwt(string login)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenValues.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: TokenValues.Issuer,
                audience: TokenValues.Audience,
                claims: new List<Claim>() { new Claim("Login", login) },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
