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
        Task<List<Annonce>> GetAnnoncesByIdVendeur(int id);
        Task<Annonce> AddAnnonce(Annonce annonce);
        Annonce GetAnnonceById(int id);
    }
    public class AnnonceService : IAnnonceServices
    {
        private IDataContext _dataContext;
        public AnnonceService (IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Annonce> AddAnnonce(Annonce annonce)
        {
            Annonce newAnnonce = _dataContext.Annonces.Add(annonce).Entity;
            await _dataContext.SaveChangesAsync();
            return newAnnonce;
        }

        public async Task<List<Annonce>> GetAllAnnoncesAsync()
        {
            return await _dataContext.Annonces.ToListAsync();
        }

        public Annonce GetAnnonceById(int id)
        {
            return _dataContext.Annonces.Where(annonce => annonce.Id == id).FirstOrDefault();
        }

        public async Task<List<Annonce>> GetAnnoncesByIdVendeur(int id)
        {
            return await _dataContext.Annonces.Where(annonce => annonce.Vendeur_id == id).ToListAsync();
        }
    }
}
