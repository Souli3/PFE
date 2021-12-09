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
    }
    public class CategorieService : ICategorieService
    {
        private IDataContext _dataContext;
        public CategorieService(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public Task<List<Categorie>> GetAllCategories()
        {
            return _dataContext.Categories.ToListAsync();
        }
    }
}
