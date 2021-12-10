using Backend.Data;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface IAnnonceAdresseService
    {
        Task<AnnonceAdresse> AddAnnonceAdresse(int idAnnonce, int idAdresse);
        List<AnnonceAdresse> GetIdAnnonceByIdAdresse(int id);
    }
    public class AnnonceAdresseService : IAnnonceAdresseService
    {
        private IDataContext _dataContext;
        public AnnonceAdresseService(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<AnnonceAdresse> AddAnnonceAdresse(int idAnnonce, int idAdresse)
        {
            AnnonceAdresse annonceAdresse = new AnnonceAdresse();
            annonceAdresse.Id = 0;
            annonceAdresse.Annonce_id = idAnnonce;
            annonceAdresse.Adresse_id = idAdresse;
            annonceAdresse = _dataContext.AnnonceAdresses.Add(annonceAdresse).Entity;
            await _dataContext.SaveChangesAsync();
            return annonceAdresse;
        }

        public List<AnnonceAdresse> GetIdAnnonceByIdAdresse(int id)
        {
            return _dataContext.AnnonceAdresses.Where(annonceAdresse => annonceAdresse.Adresse_id == id).ToList();
        }
    }
}
