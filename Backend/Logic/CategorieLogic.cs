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
        Task<Categorie> PostCategorie(Categorie categorie);
        Task DeleteCategorie(int id);
        Task<Categorie> PutCategorie(Categorie categorie, int id);
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
        public async Task<Categorie> PostCategorie(Categorie categorie)
        {
            if(categorie.Sur_categorie_id != null)
            {
                Categorie surCategorie = _CategorieServices.GetCategorie((int) categorie.Sur_categorie_id);
                if (surCategorie.Sur_categorie_id != null) 
                    throw new Exception("La sur catégorie entrée n'est pas une sur catégorie");
            }
            return await _CategorieServices.PostCategorie(categorie);
        }
        public async Task DeleteCategorie(int id)
        {
            Categorie categorieDB = _CategorieServices.GetCategorie(id);
            if(categorieDB == null) throw new Exception("La catégorie à supprimer n'a pas été trouvée");
            if (categorieDB.Sur_categorie_id == null)
            {
                await _CategorieServices.DeleteAllSubCategorie(id);
                await _CategorieServices.DeleteCategorie(id);
            }
            else 
            {
                await _CategorieServices.DeleteCategorie(id);
            }
               
        }

        public Task<Categorie> PutCategorie(Categorie categorie, int id)
        {
            Categorie categorieDB = _CategorieServices.GetCategorie(id);
            if (categorieDB == null) throw new Exception("La catégorie à modifier n'a pas été trouvée");
            return _CategorieServices.PutCategorie(categorie);
        }
    }
}
