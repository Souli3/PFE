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

        [HttpGet]
        public async Task<ActionResult<List<Categorie>>> GetAllCategories()
        {
            List<Categorie> categories = await _CategorieLogic.GetAllCategories();
            return Ok(categories);
        }
    }
}
