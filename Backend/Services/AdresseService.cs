using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface IAdresseService
    {
        Task<List<Adresse>> GetAllAdresses();
        Adresse GetAdresseByVille(string ville);
        List<Annonce> GetAllAdressesFromListAnnonces(List<Annonce> annonces);
        Annonce GetAllAdressesFromAnnonce(Annonce annonce);
        Task UpdateAdresseAnnonce(Annonce annonce);
    }
    public class AdresseService : IAdresseService
    {
        private IDataContext _dataContext;
        public AdresseService(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Adresse GetAdresseByVille(string ville)
        {
            return _dataContext.Adresses.Where(adresse => adresse.Ville == ville).FirstOrDefault();
        }

        public async Task<List<Adresse>> GetAllAdresses()
        {
            return await _dataContext.Adresses.ToListAsync();
        }
        public List<Annonce> GetAllAdressesFromListAnnonces(List<Annonce> annonces)
        {

            foreach (Annonce annonce in annonces) {
                annonce.adresses = new List<Adresse>();
                annonce.adresses= _dataContext.AnnonceAdresses.Where(x=>x.Annonce_id==annonce.Id).Join(_dataContext.Adresses,
                    annonceAdresse => annonceAdresse.Adresse_id,
                    adresse =>adresse.Id,
                    (annonceAdresse, adresse) =>                      
                            adresse).ToList();
            }
            return annonces;
        }
        public  Annonce GetAllAdressesFromAnnonce(Annonce annonce)
        {
            annonce.adresses = new List<Adresse>();
            annonce.adresses = _dataContext.AnnonceAdresses.Where(x => x.Annonce_id == annonce.Id).Join(_dataContext.Adresses,
                    annonceAdresse => annonceAdresse.Adresse_id,
                    adresse => adresse.Id,
                    (annonceAdresse, adresse) =>
                            adresse).ToList();
            return annonce;
        }

        public async Task UpdateAdresseAnnonce(Annonce annonce)
        {
            _dataContext.AnnonceAdresses.RemoveRange(_dataContext.AnnonceAdresses.Where(x => x.Annonce_id == annonce.Id));
            foreach (string adresse_idString in annonce.adressesToAdd.Split(",").ToList())
            {
                int adresseId = int.Parse(adresse_idString);
                _dataContext.AnnonceAdresses.Add(new AnnonceAdresse { Annonce_id= annonce.Id, Adresse_id = adresseId });

               
            }
            await _dataContext.SaveChangesAsync();

        }
    }
}
