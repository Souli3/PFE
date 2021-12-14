using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface ICategorieService
    {
        Task<List<Categorie>> GetAllCategories();
        Categorie GetCategorie(int id);
        Task<Categorie> PostCategorie(Categorie categorie);
        Task<Categorie> DeleteCategorie(int id);
        Task<Categorie> PutCategorie(Categorie categorie);
        Task<List<Categorie>> DeleteAllSubCategorie(int sur_categorie_id);
    }
    public class CategorieService : ICategorieService
    {
        private IDataContext _dataContext;
        public CategorieService(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Categorie>> GetAllCategories()
        {
            return await _dataContext.Categories.OrderBy(c => c.Id).ToListAsync();
        }
        public Categorie GetCategorie(int id)
        {
            return _dataContext.Categories.Where(categorie => categorie.Id == id).FirstOrDefault();
        }
        public async Task<Categorie> PostCategorie(Categorie categorie)
        {
            Categorie categorieDB = _dataContext.Categories.Add(categorie).Entity;
            await _dataContext.SaveChangesAsync();
            return categorieDB;
        }

        public async Task<Categorie> PutCategorie(Categorie categorie)
        {
            Categorie categorieDB = _dataContext.Categories.Where(c => c.Id == categorie.Id).FirstOrDefault();
            categorieDB.Nom = categorie.Nom;
            categorieDB.Sur_categorie_id = categorie.Sur_categorie_id;
            await _dataContext.SaveChangesAsync();
            return categorieDB;
        }

        public async Task<Categorie> DeleteCategorie(int id)
        {
            Categorie categorieDB = _dataContext.Categories.Where(categorie => categorie.Id == id).FirstOrDefault();
            categorieDB.Suprimee = true;
            await _dataContext.SaveChangesAsync();
            return categorieDB;
        }

        public async Task<List<Categorie>> DeleteAllSubCategorie(int sur_categorie_id)
        {
            List<Categorie> categories = await _dataContext.Categories.Where(c => c.Sur_categorie_id == sur_categorie_id).ToListAsync();  
            foreach(Categorie c in categories)
            {
                c.Suprimee = true;
            }
            await _dataContext.SaveChangesAsync();
            return categories;
        }
    }
}
