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
    public class SkillController : ControllerBase
    {
        private readonly SkillService _skillService;

        public SkillController(SkillService skillService)
        {
            _skillService = skillService;
        }
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(List<Skill>), 200)]
        public ActionResult<List<Skill>> Get() =>
          _skillService.Read();

        [AllowAnonymous]
        [HttpGet("{id:length(24)}", Name = "GetSkill")]
        [ProducesResponseType(typeof(List<Skill>), 200)]
        public ActionResult<Skill> Get(string id)
        {
            var edu = _skillService.Find(id);

            if (edu == null)
            {
                return NotFound();
            }

            return edu;
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<Skill>), 200)]
        public ActionResult<Skill> Create(Skill skill)
        {
            _skillService.Create(skill);

            return CreatedAtRoute("GetSkill", new { id = skill.Id.ToString() }, skill);
        }

        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(typeof(List<Skill>), 200)]
        public IActionResult Update(string id, Skill eduIn)
        {
            var skill = _skillService.Find(id);

            if (skill == null)
            {
                return NotFound();
            }

            _skillService.Update(id, eduIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(List<Skill>), 200)]
        public IActionResult Delete(string id)
        {
            var skill = _skillService.Find(id);

            if (skill == null)
            {
                return NotFound();
            }

            _skillService.Delete(skill.Id);

            return NoContent();
        }
    }

}