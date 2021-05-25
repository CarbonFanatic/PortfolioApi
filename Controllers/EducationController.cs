using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortfolioApi.Services;
using PortfolioApi.Models;


namespace PortfolioApi.Controllers
{   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : Controller
    {
        private readonly EducationService _educationService;

        public EducationController(EducationService educationService)
        {
            _educationService = educationService;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(List<Education>), 200)]
        public ActionResult<List<Education>> Get() =>
          _educationService.Get();

        [AllowAnonymous]
        [HttpGet("{id:length(24)}", Name = "GetEducation")]
        [ProducesResponseType(typeof(List<Education>), 200)]
        public ActionResult<Education> Get(string id)
        {
            var edu = _educationService.Find(id);

            if (edu == null)
            {
                return NotFound();
            }

            return edu;
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<Education>), 200)]
        public ActionResult<Education> Create(Education education)
        {
            _educationService.Create(education);

            return CreatedAtRoute("GetEducation", new { id = education.Id.ToString() }, education);
        }

        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(typeof(List<Education>), 200)]
        public IActionResult Update(string id, Education eduIn)
        {
            var education = _educationService.Find(id);

            if (education == null)
            {
                return NotFound();
            }

            _educationService.Update(id, eduIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(List<Education>), 200)]
        public IActionResult Delete(string id)
        {
            var education = _educationService.Find(id);

            if (education == null)
            {
                return NotFound();
            }

            _educationService.Delete(education.Id);

            return NoContent();
        }
    }

}

