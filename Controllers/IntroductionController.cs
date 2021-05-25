using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using PortfolioApi.Models;
using PortfolioApi.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace PortfolioApi.Controllers
{   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IntroductionController : ControllerBase
    {
        private readonly IntroductionService _introductionService;

        public IntroductionController(IntroductionService introductionService)
        {
            _introductionService = introductionService;
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<Introduction>> Get() =>
          _introductionService.Read();

        [AllowAnonymous]
        [HttpGet("{id:length(24)}", Name = "GetIntroduction")]
        public ActionResult<Introduction> Get(string id)
        {
            var intro = _introductionService.Find(id);

            if (intro == null)
            {
                return NotFound();
            }

            return intro;
        }

        [HttpPost]
        public ActionResult<Introduction> Create(Introduction introduction)
        {
            _introductionService.Create(introduction);

            return CreatedAtRoute("GetIntroduction", new { id = introduction.Id.ToString() }, introduction);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Introduction eduIn)
        {
            var introduction = _introductionService.Find(id);

            if (introduction == null)
            {
                return NotFound();
            }

            _introductionService.Update(id, eduIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var introduction = _introductionService.Find(id);

            if (introduction == null)
            {
                return NotFound();
            }

            _introductionService.Delete(introduction.Id);

            return NoContent();
        }
    }

}