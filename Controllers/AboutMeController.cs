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
{   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AboutMeController : ControllerBase
    {
        private readonly AboutMeService _aboutMeService;

        public AboutMeController(AboutMeService aboutMeService)
        {
            _aboutMeService = aboutMeService;
        }
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(List<AboutMe>), 200)]
        public ActionResult<List<AboutMe>> Get() =>
          _aboutMeService.Read();

        [AllowAnonymous]
        [HttpGet("{id:length(24)}", Name = "GetAboutMe")]
        [ProducesResponseType(typeof(List<AboutMe>), 200)]
        public ActionResult<AboutMe> Get(string id)
        {
            var edu = _aboutMeService.Find(id);

            if (edu == null)
            {
                return NotFound();
            }

            return edu;
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<AboutMe>), 200)]
        public ActionResult<AboutMe> Create(AboutMe aboutMe)
        {
            _aboutMeService.Create(aboutMe);

            return CreatedAtRoute("GetAboutMe", new { id = aboutMe.Id.ToString() }, aboutMe);
        }

        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(typeof(List<AboutMe>), 200)]
        public IActionResult Update(string id, AboutMe eduIn)
        {
            var aboutMe = _aboutMeService.Find(id);

            if (aboutMe == null)
            {
                return NotFound();
            }

            _aboutMeService.Update(id, eduIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(List<AboutMe>), 200)]
        public IActionResult Delete(string id)
        {
            var aboutMe = _aboutMeService.Find(id);

            if (aboutMe == null)
            {
                return NotFound();
            }

            _aboutMeService.Delete(aboutMe.Id);

            return NoContent();
        }
    }

}