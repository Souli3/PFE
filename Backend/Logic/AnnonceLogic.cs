using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Logic
{
    public interface IAnnonceLogic
    {
        Task<List<Annonce>> GetAllAnnonces();
        Task<List<Annonce>> GetAnnoncesByEmail(String email);
        Task<Annonce> AddAnnonce(Annonce annonce);
        Annonce GetAnnonceById(int id);
        Task<ActionResult<Annonce>> UpdateAnnonce(Annonce annonce);
        Task<List<Annonce>> GetAllAnnoncesByCategorie(Categorie categorie);
        Task<List<Annonce>> GetAnnoncesByCampusName(string campusName);
    }
    public class AnnonceLogic : IAnnonceLogic
    {
        private IAnnonceServices _AnnonceServices;
        private IMembreServices _MembreServices;
        private IAdresseService _AdresseServices;
        private IAnnonceAdresseService _AnnonceAdresseService;
        public AnnonceLogic(IAnnonceServices AnnonceServices, IMembreServices MembreServices,
            IAdresseService AdresseServices, IAnnonceAdresseService AnnonceAdresseService)
        {
            _AnnonceServices = AnnonceServices;
            _MembreServices = MembreServices;
            _AdresseServices = AdresseServices;
            _AnnonceAdresseService = AnnonceAdresseService;
        }

        public async Task<Annonce> AddAnnonce(Annonce annonce)
        {
            Membre membre = _MembreServices.GetMembre(annonce.Vendeur_id);
            if (membre == null)
                throw new Exception("Membre invalide");
            Annonce newAnnonce = await _AnnonceServices.AddAnnonce(annonce);
            await _AnnonceAdresseService.AddAnnonceAdresse(newAnnonce.Id, membre.Campus_Id);
            return newAnnonce;
        }

        public async Task<List<Annonce>> GetAllAnnonces()
        {
            return await _AnnonceServices.GetAllAnnoncesAsync();
        }

        public async Task<List<Annonce>> GetAllAnnoncesByCategorie(Categorie categorie)
        {
            return await _AnnonceServices.GetAllAnnoncesByCategorie(categorie);
        }

        public Annonce GetAnnonceById(int id)
        {
            return _AnnonceServices.GetAnnonceById(id); 
        }

        public async Task<List<Annonce>> GetAnnoncesByCampusName(string campusName)
        {
            Adresse adresse = _AdresseServices.GetAdresseByVille(campusName);
            if (adresse == null)
                throw new Exception("Aucune adresse trouvée pour ce nom de campus");

            //List<AnnonceAdresse> annoncesAdresses = _AnnonceAdresseService.GetIdAnnonceByIdAdresse();
            //if (annoncesAdresses == null)
            //    throw new Exception("Aucune annonce trouvée pour ce campus");

            return await _AnnonceServices.GetAnnoncesByIdAdresse(adresse.Id);
        }

        public async Task<List<Annonce>> GetAnnoncesByEmail(String email)
        {
            Membre membre = _MembreServices.GetMembreByEmail(email);
            if(membre == null)
            {
                throw new Exception("Membre non trouvé");
            }
            return await _AnnonceServices.GetAnnoncesByIdVendeur(membre.Id);
        }

        public async Task<ActionResult<Annonce>> UpdateAnnonce(Annonce annonce)
        {
            return await _AnnonceServices.UpdateAnnonce(annonce);
        }
    }
}
