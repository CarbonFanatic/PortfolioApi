using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioApi.Models;
using PortfolioApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioApi.Controllers
{   
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<User>), 200)]
        public ActionResult<List<User>> Get() =>
          _userService.Read();

        [HttpGet("{id:length(24)}", Name = "GetUser")]
        [ProducesResponseType(typeof(List<User>), 200)]
        public ActionResult<User> Get(string id)
        {
            var edu = _userService.Find(id);

            if (edu == null)
            {
                return NotFound();
            }

            return edu;
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<User>), 200)]
        public ActionResult<User> Create(User user)
        {
            _userService.Create(user);

            return CreatedAtRoute("GetUser", new { id = user.Id.ToString() }, user);
        }

        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(typeof(List<User>), 200)]
        public IActionResult Update(string id, User eduIn)
        {
            var user = _userService.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            _userService.Update(id, eduIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(List<User>), 200)]
        public IActionResult Delete(string id)
        {
            var user = _userService.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            _userService.Delete(user.Id);

            return NoContent();
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public ActionResult Login([FromBody] User user)
        {
            var token = _userService.Authenticate(user.Email, user.Password);
            if (token == null)
                return Unauthorized();
            return Ok(new { token, user });
        }
    }

}