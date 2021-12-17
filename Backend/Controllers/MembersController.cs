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
            Membre m = _MembreLogic.GetMembre(_jwtTokenManager.DecodeJWTToGetEmail(Request));
            m.MotDePasse = "";
            return Ok(m);
        }
        [HttpGet("GetMembre/{id}")]
        public ActionResult<Membre> GetMemberById(int id)
        {
            Membre m = _MembreLogic.GetMembreById(id);
            m.MotDePasse = "";
            return Ok(m);
        }
        [HttpPost("GetMembre/email")]
        public ActionResult<int> GetMemberByEmail(Membre membre)
        {
            Membre m = _MembreLogic.GetMembre(membre.Email);
            return Ok(m.Id);
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
        
        [Authorize(Roles = "Admin")]
        [HttpPut("ban/{id}/{time}")]
        public async Task<ActionResult<Membre>> BanMembre(int id, int time)
        {
            Membre bannedMember;
            try
            {
                bannedMember = await _MembreLogic.BanMembre(id, time);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok(bannedMember);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("admin/{id}")]
        public async Task<ActionResult<Membre>> PutAdmin(int id)
        {
            Membre admin;
            try
            {
                admin = await _MembreLogic.PutAdmin(id);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok(admin);
        }
    }
}
