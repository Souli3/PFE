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
        [HttpGet]
        public IActionResult Login(Personnes personnes)
        {
            var token = _jwtTokenManager.Authenticate(personnes.Name, personnes.MotDePasse);
            if (string.IsNullOrEmpty(token)) return Unauthorized();
            return Ok(token);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(Personnes personnes)
        {
            Personnes personneInscrite = await _registerLogic.Register(personnes);
            if (personneInscrite == null) return BadRequest();


            var token = _jwtTokenManager.Authenticate(personneInscrite.Name, personneInscrite.MotDePasse);
            if (string.IsNullOrEmpty(token)) return Unauthorized();
            return Ok(token);
        }

    }
}
