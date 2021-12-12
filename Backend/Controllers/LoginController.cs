﻿using Backend.Authentification;
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
        private IMailLogic _mailLogic;
        public LoginController(IJwtTokenManager jwtTokenManager, IRegisterLogic registerLogic, IMailLogic mailLogic)
        {
            _jwtTokenManager = jwtTokenManager;
            _registerLogic = registerLogic;
            _mailLogic = mailLogic;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(Membre membre)
        {
            Membre membreDB = _registerLogic.Login(membre);
            if (membreDB.Banni > DateTime.Now) return Unauthorized("Vous êtes banni");

            if (membreDB == null || membreDB.Email == null) return Unauthorized();

            if (!BCrypt.Net.BCrypt.Verify(membre.MotDePasse, membreDB.MotDePasse)) return Unauthorized();
           
            var token = _jwtTokenManager.Authenticate(membre.Email, membre.MotDePasse);
            if (string.IsNullOrEmpty(token)) return Unauthorized();
            membreDB.Token = token;
            membreDB.MotDePasse = "";
            return Ok(membreDB);
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(Membre membre)
        {
            membre.MotDePasse = BCrypt.Net.BCrypt.HashPassword(membre.MotDePasse);
            Membre membreInscrit = await _registerLogic.Register(membre);
            if (membreInscrit == null) return BadRequest();

            _mailLogic.Send(membreInscrit.Email);

            var token = _jwtTokenManager.Authenticate(membreInscrit.Email, membreInscrit.MotDePasse);
            if (string.IsNullOrEmpty(token)) return Unauthorized();
            membreInscrit.Token = token;
            membreInscrit.MotDePasse = "";
            return Ok(membreInscrit);
        }

    }
}
