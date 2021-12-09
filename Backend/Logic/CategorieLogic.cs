using Backend.Models;
using Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Logic
{
    public interface ICategorieLogic
    {
        Task<List<Categorie>> GetAllCategories();
    }
    public class CategorieLogic : ICategorieLogic
    {
        private ICategorieService _CategorieServices;
        public CategorieLogic(ICategorieService CategorieServices)
        {
            _CategorieServices = CategorieServices;
        }

        public async Task<List<Categorie>> GetAllCategories()
        {
            return await _CategorieServices.GetAllCategories();
        }
    }
}
