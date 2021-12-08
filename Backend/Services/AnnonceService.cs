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
        Task<List<Annonce>> GetAnnoncesById(int id);
        Task AddAnnonce(Annonce annonce);
    }
    public class AnnonceService : IAnnonceServices
    {
        private IDataContext _dataContext;
        public AnnonceService (IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddAnnonce(Annonce annonce)
        {
            _dataContext.Annonces.Add(annonce);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<Annonce>> GetAllAnnoncesAsync()
        {
            return await _dataContext.Annonces.ToListAsync();
        }

        public async Task<List<Annonce>> GetAnnoncesById(int id)
        {
            return await _dataContext.Annonces.Where(x => x.Vendeur_id == id).ToListAsync();
        }
    }
}
