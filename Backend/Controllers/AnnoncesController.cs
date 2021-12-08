using Backend.Authentification;
using Backend.Logic;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AnnoncesController : ControllerBase
    {
        private IAnnonceLogic _AnnonceLogic;
        private readonly IJwtTokenManager _jwtTokenManager;

        public AnnoncesController(IAnnonceLogic AnnonceLogic, IJwtTokenManager jwtTokenManager)
        {
            _AnnonceLogic = AnnonceLogic;
            _jwtTokenManager = jwtTokenManager;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Annonce>>> GetAllAnnonces()
        {
            List<Annonce> annonces = await _AnnonceLogic.GetAllAnnonces();
            return Ok(annonces);
        }

        [HttpGet("email")]
        public async Task<ActionResult<List<Annonce>>> GetAnnoncesByEmail()
        {
            String email = _jwtTokenManager.DecodeJWTToGetEmail(Request);
            List<Annonce> annonces;
            try
            {
                annonces = await _AnnonceLogic.GetAnnoncesByEmail(email);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok(annonces);
        }

        [HttpPost]
        public async Task<ActionResult> AddAnnonce(Annonce annonce)
        {
            Annonce newAnnonce = await _AnnonceLogic.AddAnnonce(annonce);
            return Ok(newAnnonce);
        }
    }
}
