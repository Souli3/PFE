using Backend.Authentification;
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
        private readonly IJwtTokenManager _jwtTokenManager;
        public MembersController(IMembresLogic MembreLogic, IJwtTokenManager jwtTokenManager)
        {
            _MembreLogic = MembreLogic;
            _jwtTokenManager = jwtTokenManager;
        }
        [HttpGet("GetMembre")]
        public ActionResult<Membre> GetMember()
        {
            Membre p = _MembreLogic.GetMembre(_jwtTokenManager.DecodeJWTToGetEmail(Request));
            p.MotDePasse = "";
            return Ok(p);
        }
        [HttpPut("UpdateMembre")]
        public async Task<ActionResult<Membre>> UpdateMember(Membre membre)
        {
            if (membre == null || !_jwtTokenManager.DecodeJWTToGetEmail(Request).Equals(membre.Email)) return BadRequest();
            membre = await _MembreLogic.UpdateMember(membre);
            return Ok(membre);
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
