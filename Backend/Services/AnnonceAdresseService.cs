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
        Task AddAnnonceAdresses(Annonce annonce);
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
        public async Task AddAnnonceAdresses(Annonce annonce)
        {
            List<string> listsAdresses = annonce.adressesToAdd.Split(',').ToList();

            foreach (string adresse_id in listsAdresses)
            {
                if (!_dataContext.AnnonceAdresses.Any(x => x.Adresse_id.ToString().Equals(adresse_id) && x.Annonce_id == annonce.Id)) { 
                    _dataContext.AnnonceAdresses.Add(new AnnonceAdresse() { Annonce_id = annonce.Id, Adresse_id = int.Parse(adresse_id) });
                    await _dataContext.SaveChangesAsync();
                }
            }
            
        }
    }
}
