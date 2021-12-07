using Backend.Authentification;
using Backend.Logic;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IJwtTokenManager _jwtTokenManager;
        private IRegisterLogic _registerLogic;
        public LoginController(IJwtTokenManager jwtTokenManager, IRegisterLogic registerLogic)
        {
            _jwtTokenManager = jwtTokenManager;
            _registerLogic = registerLogic;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(Membre membres)
        {
            var token = _jwtTokenManager.Authenticate(membres.Email, membres.MotDePasse);
            if (string.IsNullOrEmpty(token)) return Unauthorized();
            return Ok(token);
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(Membre membres)
        {
            Membre membresInscrite = await _registerLogic.Register(membres);
            if (membresInscrite == null) return BadRequest();


            var token = _jwtTokenManager.Authenticate(membresInscrite.Email, membresInscrite.MotDePasse);
            if (string.IsNullOrEmpty(token)) return Unauthorized();
            return Ok(token);
        }

    }
}
