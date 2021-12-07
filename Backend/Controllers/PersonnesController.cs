using Backend.Data;
using Backend.Logic;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PersonnesController: ControllerBase
    {
        private IPersonnesLogic _personnesLogic;
        public PersonnesController(IPersonnesLogic personnesLogic)
        {
            _personnesLogic = personnesLogic;
        }
        [HttpGet("{id}")]
        public ActionResult<Personnes> GetPersonnes(int id)
        {
            Personnes p = _personnesLogic.GetPersonnes(id);
            return Ok(p);
        }
        [HttpGet]
        public async Task<ActionResult<List<Personnes>>> GetAllPersonnes()
        {
            List<Personnes> pes = await _personnesLogic.GetAllPersonnes();
            return Ok(pes);
        }
        [HttpPost]
        public async Task<ActionResult> AddPersonne(Personnes personnes)
        {
            Personnes p = personnes;
            await _personnesLogic.AddPersonne(personnes);

            return Ok();
        }
    }
}
