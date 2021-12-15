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
        Task<List<Annonce>> GetAllAnnoncesStatusE();
        Task<List<Annonce>> GetAnnoncesByEmail(String email);
        Task<Annonce> AddAnnonce(Annonce annonce);
        Task<Annonce> GetAnnonceById(int id);
        Task<Annonce> UpdateAnnonce(Annonce annonce, string emailVendeurDecoder);
        Task<List<Annonce>> GetAllAnnoncesByCategorie(Categorie categorie);
        Task<List<Annonce>> GetAnnoncesByCampusName(string campusName);
        Task<Annonce> UpdateAnnonceAdmin(Annonce annonce);
    }
    public class AnnonceLogic : IAnnonceLogic
    {
        private IAnnonceServices _AnnonceServices;
        private IMembreServices _MembreServices;
        private IAdresseService _AdresseServices;
        private IAnnonceAdresseService _AnnonceAdresseService;
        private IMediaService _MediaService;
        public AnnonceLogic(IAnnonceServices AnnonceServices, IMembreServices MembreServices,
            IAdresseService AdresseServices, IAnnonceAdresseService AnnonceAdresseService, IMediaService mediaService)
        {
            _AnnonceServices = AnnonceServices;
            _MembreServices = MembreServices;
            _AdresseServices = AdresseServices;
            _AnnonceAdresseService = AnnonceAdresseService;
            _MediaService = mediaService;
        }

        public async Task<Annonce> AddAnnonce(Annonce annonce)
        {
            Membre membre = _MembreServices.GetMembre(annonce.Vendeur_id);
            if (membre == null)
                throw new Exception("Membre invalide");
            Annonce newAnnonce = await _AnnonceServices.AddAnnonce(annonce);
            //await _AnnonceAdresseService.AddAnnonceAdresse(newAnnonce.Id, membre.Campus_Id);
            if (annonce.adressesToAdd != null)
            {
                await _AnnonceAdresseService.AddAnnonceAdresses(annonce);
            }
            return await _MediaService.GetAllMediaFromAnnonce(newAnnonce);
        }

        public async Task<List<Annonce>> GetAllAnnonces()
        {
            List<Annonce> annonces = await _AnnonceServices.GetAllAnnoncesAsync();
            if (annonces == null) return null;
            return  _AdresseServices.GetAllAdressesFromListAnnonces( await _MediaService.GetAllMediaFromListAnnonce(annonces));
        }

        public async Task<List<Annonce>> GetAllAnnoncesByCategorie(Categorie categorie)
        {
            List<Annonce> annonces = await _AnnonceServices.GetAllAnnoncesByCategorie(categorie);
            if (annonces == null) return null;
            return  _AdresseServices.GetAllAdressesFromListAnnonces( await _MediaService.GetAllMediaFromListAnnonce(annonces));
        }

        public async Task<List<Annonce>> GetAllAnnoncesStatusE()
        {
            List<Annonce> annonces = await _AnnonceServices.GetAllAnnoncesStatusE();
            if (annonces == null) return null;
            return _AdresseServices.GetAllAdressesFromListAnnonces(await _MediaService.GetAllMediaFromListAnnonce(annonces));
        }

        public async Task<Annonce> GetAnnonceById(int id)
        {
            Annonce annonce = _AnnonceServices.GetAnnonceById(id);
            if (annonce == null) return null;
            return  _AdresseServices.GetAllAdressesFromAnnonce( await _MediaService.GetAllMediaFromAnnonce(annonce)); 
        }

        public async Task<List<Annonce>> GetAnnoncesByCampusName(string campusName)
        {
            Adresse adresse = _AdresseServices.GetAdresseByVille(campusName);
            if (adresse == null)
                throw new Exception("Aucune adresse trouvée pour ce nom de campus");
            List<Annonce> annonces = await _AnnonceServices.GetAnnoncesByIdAdresse(adresse.Id);
            if (annonces == null) return null;
            return  _AdresseServices.GetAllAdressesFromListAnnonces( await _MediaService.GetAllMediaFromListAnnonce(annonces));
        }

        public async Task<List<Annonce>> GetAnnoncesByEmail(String email)
        {
            Membre membre = _MembreServices.GetMembreByEmail(email);
            if(membre == null)
            {
                throw new Exception("Membre non trouvé");
            }
            List<Annonce> annonces = await _AnnonceServices.GetAnnoncesByIdVendeur(membre.Id);
            if (annonces == null) return null;
            return _AdresseServices.GetAllAdressesFromListAnnonces( await _MediaService.GetAllMediaFromListAnnonce(annonces));
        }

        public async Task<Annonce> UpdateAnnonce(Annonce annonce, string emailVendeurDecoder)
        {

            if (annonce.Vendeur_id != _MembreServices.GetMembreByEmail(emailVendeurDecoder).Id || annonce.Titre == "" || annonce.Description == "" || annonce.Prix ==null  || annonce.Prix < 0 || !annonce.Etat.HasValue || annonce.Categorie_id==null)
            {
                return null;
            }
            if (annonce.Etat == 'V')
            {
                Annonce annonceDB = _AnnonceServices.GetAnnonceById(annonce.Id);
                if(annonceDB.Etat == 'E')
                {
                    return null;
                }
            }
            if(annonce.adressesToAdd!=null &&  annonce.adressesToAdd.Length>0)
            {
                await _AdresseServices.UpdateAdresseAnnonce(annonce);
            }
            return await _AnnonceServices.UpdateAnnonce(annonce);
        }

        public async Task<Annonce> UpdateAnnonceAdmin(Annonce annonce)
        {
            return await _AnnonceServices.UpdateAnnonce(annonce);
        }
    }
}
