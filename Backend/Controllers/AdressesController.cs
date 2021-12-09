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
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AdressesController : ControllerBase
    {
        private IAdresseLogic _AdresseLogic;

        public AdressesController(IAdresseLogic AdresseLogic)
        {
            _AdresseLogic = AdresseLogic;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Adresse>>> GetAllAdresses()
        {
            List<Adresse> adresses = await _AdresseLogic.GetAllAdresses();
            return Ok(adresses);
        }
    }
}
