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
        Task<Categorie> PutCategorie(Categorie categorie);
        Task<Categorie> DeleteCategorie(int id);
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
        public async Task<Categorie> PutCategorie(Categorie categorie)
        {
            if(categorie.Sur_categorie_id != null)
            {
                Categorie surCategorie = _CategorieServices.GetCategorie((int) categorie.Sur_categorie_id);
                if (surCategorie.Sur_categorie_id != null) 
                    throw new Exception("La sur catégorie entrée n'est pas une sur catégorie");
            }
            return await _CategorieServices.PutCategorie(categorie);
        }
        public async Task<Categorie> DeleteCategorie(int id)
        {
            Categorie categorieDB = _CategorieServices.GetCategorie(id);
            if (categorieDB == null)
                throw new Exception("Il n'y a pas de catégorie pour cet ID");
            return await _CategorieServices.DeleteCategorie(categorieDB);
        }
    }
}
