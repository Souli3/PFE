using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface IAnnonceServices
    {
        Task<List<Annonce>> GetAllAnnoncesAsync();
    }
    public class AnnonceService : IAnnonceServices
    {
        private IDataContext _dataContext;
        public AnnonceService (IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<List<Annonce>> GetAllAnnoncesAsync()
        {
            return await _dataContext.Annonces.ToListAsync();
        }
    }
}
