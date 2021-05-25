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
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectsService _projectsService;

        public ProjectsController(ProjectsService projectsService)
        {
            _projectsService = projectsService;
        }
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(List<Projects>), 200)]
        public ActionResult<List<Projects>> Get() =>
          _projectsService.Read();
        [AllowAnonymous]
        [HttpGet("{id:length(24)}", Name = "GetProjects")]
        [ProducesResponseType(typeof(List<Projects>), 200)]
        public ActionResult<Projects> Get(string id)
        {
            var edu = _projectsService.Find(id);

            if (edu == null)
            {
                return NotFound();
            }

            return edu;
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<Projects>), 200)]
        public ActionResult<Projects> Create(Projects projects)
        {
            _projectsService.Create(projects);

            return CreatedAtRoute("GetProjects", new { id = projects.Id.ToString() }, projects);
        }

        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(typeof(List<Projects>), 200)]
        public IActionResult Update(string id, Projects eduIn)
        {
            var projects = _projectsService.Find(id);

            if (projects == null)
            {
                return NotFound();
            }

            _projectsService.Update(id, eduIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(List<Projects>), 200)]
        public IActionResult Delete(string id)
        {
            var projects = _projectsService.Find(id);

            if (projects == null)
            {
                return NotFound();
            }

            _projectsService.Delete(projects.Id);

            return NoContent();
        }
    }

}