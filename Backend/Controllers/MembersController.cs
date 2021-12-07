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
    public class MembersController: ControllerBase
    {
        private IMembresLogic _MembreLogic;
        public MembersController(IMembresLogic MembreLogic)
        {
            _MembreLogic = MembreLogic;
        }
        [HttpGet("{id}")]
        public ActionResult<Membre> GetMembre(int id)
        {
            Membre p = _MembreLogic.GetMembre(id);
            return Ok(p);
        }
        [HttpGet]
        public async Task<ActionResult<List<Membre>>> GetAllMembre()
        {
            List<Membre> pes = await _MembreLogic.GetAllMembres();
            return Ok(pes);
        }
        [HttpPost]
        public async Task<ActionResult> AddPersonne(Membre Membre)
        {
            Membre p = Membre;
            await _MembreLogic.AddMembre(Membre);

            return Ok();
        }
    }
}
