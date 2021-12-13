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
        Task<Categorie> PutCategorie(Categorie categorie);
        Task<Categorie> DeleteCategorie(Categorie categorie);
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
            return await _dataContext.Categories.ToListAsync();
        }
        public Categorie GetCategorie(int id)
        {
            return _dataContext.Categories.Where(categorie => categorie.Id == id).FirstOrDefault();
        }
        public async Task<Categorie> PutCategorie(Categorie categorie)
        {
            Categorie categorieDB = _dataContext.Categories.Add(categorie).Entity;
            await _dataContext.SaveChangesAsync();
            return categorieDB;
        }
        public async Task<Categorie> DeleteCategorie(Categorie categorie)
        {
            Categorie categorieDB = _dataContext.Categories.Remove(categorie).Entity;
            await _dataContext.SaveChangesAsync();
            return categorieDB;
        }

    }
}
