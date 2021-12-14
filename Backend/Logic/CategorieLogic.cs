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
        private IAnnonceServices _AnnonceServices;
        public CategorieLogic(ICategorieService CategorieServices, IAnnonceServices AnnonceServices)
        {
            _CategorieServices = CategorieServices;
            _AnnonceServices = AnnonceServices;
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
            List<Categorie> deletedCategories;
            if (categorieDB.Sur_categorie_id == null)
            {
                deletedCategories = await _CategorieServices.DeleteAllSubCategorie(id);
                deletedCategories.Add(await _CategorieServices.DeleteCategorie(id));
            }
            else 
            {
                deletedCategories = new List<Categorie>();
                deletedCategories.Add(await _CategorieServices.DeleteCategorie(id));
            }
            await _AnnonceServices.DeleteCategories(deletedCategories);
        }

        public Task<Categorie> PutCategorie(Categorie categorie, int id)
        {
            Categorie categorieDB = _CategorieServices.GetCategorie(id);
            if (categorieDB == null) throw new Exception("La catégorie à modifier n'a pas été trouvée");
            return _CategorieServices.PutCategorie(categorie);
        }
    }
}
