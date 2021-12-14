using Backend.Authentification;
using Backend.Logic;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
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
        private IMailLogic _mailLogic;
        public LoginController(IJwtTokenManager jwtTokenManager, IRegisterLogic registerLogic, IMailLogic mailLogic)
        {
            _jwtTokenManager = jwtTokenManager;
            _registerLogic = registerLogic;
            _mailLogic = mailLogic;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(Membre membre)
        {
            Membre membreDB = _registerLogic.Login(membre);
            if (membreDB.Valide == false) return Unauthorized("Vous n'avez pas encore valider votre compte");
            if (membreDB.Banni > DateTime.Now) return Unauthorized("Vous êtes banni");

            if (membreDB == null || membreDB.Email == null) return Unauthorized();

            if (!BCrypt.Net.BCrypt.Verify(membre.MotDePasse, membreDB.MotDePasse)) return Unauthorized();
           
            var token = _jwtTokenManager.Authenticate(membre.Email, membre.MotDePasse);
            if (string.IsNullOrEmpty(token)) return Unauthorized("Compte inexistant !");
            membreDB.Token = token;
            membreDB.MotDePasse = "";
            return Ok(membreDB);
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(Membre membre)
        {
            if (membre == null || membre.Email=="" || membre.MotDePasse=="") return BadRequest("Veuillez rentrer un email et mot de passe valide ");
            if (!Regex.IsMatch(membre.Email, @"^[A-Za-z]+[\.][A-Za-z]+[@]([A-Za-z]+[\.]){0,1}(vinci)[\.][a-z]{2,3}$")) return BadRequest("Email invalide");
            membre.MotDePasse = BCrypt.Net.BCrypt.HashPassword(membre.MotDePasse);
            Membre membreInscrit = await _registerLogic.Register(membre);
            if (membreInscrit == null) return BadRequest("Cette email existe deja !");

            _mailLogic.Send(membreInscrit);
            return Ok("Nous vous avons envoyé un mail afin de valider votre compte");
        }

        [AllowAnonymous]
        [HttpGet("validate/{id}")]
        public async Task<IActionResult> Validate(int id)
        {
            Membre membreDB = await _registerLogic.Validate(id);
            if (membreDB == null) return NotFound("Compte à valider non trouvé");
            return Ok("Votre compte est maintenant validé !");
        }

    }
}
