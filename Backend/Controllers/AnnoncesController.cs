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
    public class AnnoncesController : ControllerBase
    {
        private IAnnonceLogic _AnnonceLogic;

        public AnnoncesController(IAnnonceLogic AnnonceLogic)
        {
            _AnnonceLogic = AnnonceLogic;
        }

        [HttpGet]
        public async Task<ActionResult<List<Annonce>>> GetAllAnnonces()
        {
            List<Annonce> annonces = await _AnnonceLogic.GetAllAnnonces();
            return Ok(annonces);
        }
    }
}
