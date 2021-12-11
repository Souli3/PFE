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

        public AnnoncesController(IAnnonceLogic AnnonceLogic, IJwtTokenManager jwtTokenManager, IWebHostEnvironment hostEnvironment, IMediaLogic mediaLogic)
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Annonce>> GetAnnonceById(int id)
        {
            Annonce annonce = await _AnnonceLogic.GetAnnonceById(id);
            return Ok(annonce);
        }

        [AllowAnonymous]
        [HttpGet("campus/{name}")]
        public async Task<ActionResult<List<Annonce>>>GetAnnoncesByCampusName(String name)
        {
            List<Annonce> annonce;
            try
            {
                annonce = await _AnnonceLogic.GetAnnoncesByCampusName(name);
            }
            catch(Exception e)
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
        public async Task<ActionResult> AddAnnonce([FromForm] Annonce annonce)
        {
           Annonce newAnnonce;
            try
            {
                newAnnonce = await _AnnonceLogic.AddAnnonce(annonce); 
            } 
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
            
            List<string> mesPhotos = new  List<String>();

            
                annonce.ImageFile.ForEach( imageFile =>  {
                    mesPhotos.Add( SaveImageAsync(imageFile));
                 });
            
            await _mediaLogic.AddMedias(mesPhotos, newAnnonce.Id);


            return Ok("Votre annonce a été ajouté avec succès");
        }

        [HttpPut]
        public async Task<ActionResult<Annonce>> UpdateAnnonce(Annonce annonce)
        {
            return await _AnnonceLogic.UpdateAnnonce(annonce);
        }
        [NonAction]
        public  string SaveImageAsync(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                 imageFile.CopyTo(fileStream);
            }
            return imageName;
        }
    }
}
