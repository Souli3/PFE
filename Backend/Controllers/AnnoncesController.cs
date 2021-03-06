using Backend.Authentification;
using Backend.Logic;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
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
        private IWebHostEnvironment _hostEnvironment;
        private IMediaLogic _mediaLogic;

        public AnnoncesController(IAnnonceLogic AnnonceLogic, IJwtTokenManager jwtTokenManager, IMediaLogic mediaLogic, IWebHostEnvironment hostEnvironment)
        {
            _AnnonceLogic = AnnonceLogic;
            _jwtTokenManager = jwtTokenManager;
            _hostEnvironment = hostEnvironment;
            _mediaLogic = mediaLogic;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Annonce>>> GetAllAnnonces()
        {
            List<Annonce> annonces = await _AnnonceLogic.GetAllAnnonces();
            return Ok(annonces);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllAnnoncesStatusE")]
        public async Task<ActionResult<List<Annonce>>> GetAllAnnoncesStatusE()
        {
            return Ok(await _AnnonceLogic.GetAllAnnoncesStatusE());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Annonce>> GetAnnonceById(int id)
        {
            Annonce annonce = await _AnnonceLogic.GetAnnonceById(id);
            if (annonce == null) return NotFound("Annonce introuvable !");
            return Ok(annonce);
        }

        [AllowAnonymous]
        [HttpGet("campus/{name}")]
        public async Task<ActionResult<List<Annonce>>> GetAnnoncesByCampusName(String name)
        {
            List<Annonce> annonce;
            try
            {
                annonce = await _AnnonceLogic.GetAnnoncesByCampusName(name);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok(annonce);
        }

        [AllowAnonymous]
        [HttpGet("categorie")]
        public async Task<ActionResult<List<Annonce>>> GetAllAnnoncesByCategorie(Categorie categorie)
        {
            List<Annonce> annonces = await _AnnonceLogic.GetAllAnnoncesByCategorie(categorie);
            if (annonces == null) return NotFound("Aucune annonce trouvé");
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
        public async Task<ActionResult<int>> AddAnnonce([FromForm] Annonce annonce)
        {
            Annonce newAnnonce;
            try
            {
                newAnnonce = await _AnnonceLogic.AddAnnonce(annonce);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            
            if (annonce.ImageFile != null) { 
                List<string> mesPhotos = new List<String>();
                annonce.ImageFile.ForEach(imageFile => {
                    mesPhotos.Add(SaveImageAsync(imageFile));
                });

                await _mediaLogic.AddMedias(mesPhotos, newAnnonce.Id);
            }

            return Ok(newAnnonce.Id);
        }

        [HttpPut]
        public async Task<ActionResult<Annonce>> UpdateAnnonce(Annonce annonce)
        {
            
            Annonce annonceDB = await _AnnonceLogic.UpdateAnnonce(annonce, _jwtTokenManager.DecodeJWTToGetEmail(Request));
            if (annonceDB == null) return Unauthorized("Il faut être admin pour valider une annonce");
            return Ok(annonceDB);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("admin")]
        public async Task<ActionResult<Annonce>> UpdateAnnonceAdmin(Annonce annonce)
        {
            Annonce annonceDB = await _AnnonceLogic.UpdateAnnonceAdmin(annonce);
            return Ok(annonceDB);
        }

        [NonAction]
        public string SaveImageAsync(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Medias", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                 imageFile.CopyTo(fileStream);
            }
            return imageName;
        }
    }
}
