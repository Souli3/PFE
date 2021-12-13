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
    public class CategoriesController : ControllerBase
    {
        private ICategorieLogic _CategorieLogic;

        public CategoriesController(ICategorieLogic CategorieLogic)
        {
            _CategorieLogic = CategorieLogic;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Categorie>>> GetAllCategories()
        {
            List<Categorie> categories = await _CategorieLogic.GetAllCategories();
            return Ok(categories);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Categorie>> PostCategorie(Categorie categorie)
        {
            Categorie categorieDB;
            try
            {
                categorieDB = await _CategorieLogic.PostCategorie(categorie);
            }
            catch (Exception e)
            {
                return (Unauthorized(e.Message));
            }

            return Ok(categorieDB);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Categorie>> PutCategorie(Categorie categorie, int id)
        {
            categorie.Id = id;
            Categorie categorieDB;
            try
            {
                categorieDB = await _CategorieLogic.PutCategorie(categorie, id);
            }
            catch (Exception e)
            {
                return (NotFound(e.Message));
            }

            return Ok(categorieDB);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("delete/{id}")]
        public async Task<ActionResult<Categorie>> DeleteCategorie(int id)
        {
            try
            {
                await _CategorieLogic.DeleteCategorie(id);
            }
            catch (Exception e)
            {
                return (NotFound(e.Message));
            }

            return Ok("La catégorie a bien été supprimée");
        }
    }
}
