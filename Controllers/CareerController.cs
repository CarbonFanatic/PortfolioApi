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
    public class CareerController : ControllerBase
    {
    
    private readonly CareerService _careerService;

    public CareerController(CareerService careerService)
    {
        _careerService = careerService;
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(List<Career>), 200)]
    public ActionResult<List<Career>> Get() =>
      _careerService.Read();

    [AllowAnonymous]
    [HttpGet("{id:length(24)}", Name = "GetCareer")]
    [ProducesResponseType(typeof(List<Career>), 200)]
    public ActionResult<Career> Get(string id)
    {
        var edu = _careerService.Find(id);

        if (edu == null)
        {
            return NotFound();
        }

        return edu;
    }

    [HttpPost]
    [ProducesResponseType(typeof(List<Career>), 200)]
    public ActionResult<Career> Create(Career career)
    {
        _careerService.Create(career);

        return CreatedAtRoute("GetCareer", new { id = career.Id.ToString() }, career);
    }

    [HttpPut("{id:length(24)}")]
    [ProducesResponseType(typeof(List<Career>), 200)]
    public IActionResult Update(string id, Career eduIn)
    {
        var career = _careerService.Find(id);

        if (career == null)
        {
            return NotFound();
        }

        _careerService.Update(id, eduIn);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    [ProducesResponseType(typeof(List<Career>), 200)]
    public IActionResult Delete(string id)
    {
        var career = _careerService.Find(id);

        if (career == null)
        {
            return NotFound();
        }

        _careerService.Delete(career.Id);

        return NoContent();
    }
}

}