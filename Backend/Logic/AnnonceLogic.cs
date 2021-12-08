using Backend.Models;
using Backend.Services;
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
    }
    public class AnnonceLogic : IAnnonceLogic
    {
        private IAnnonceServices _AnnonceServices;
        private IMembreServices _MembreServices;
        public AnnonceLogic(IAnnonceServices AnnonceServices, IMembreServices MembreServices)
        {
            _AnnonceServices = AnnonceServices;
            _MembreServices = MembreServices;
        }

        public async Task<Annonce> AddAnnonce(Annonce annonce)
        {
            return await _AnnonceServices.AddAnnonce(annonce);
        }

        public async Task<List<Annonce>> GetAllAnnonces()
        {
            return await _AnnonceServices.GetAllAnnoncesAsync();
        }

        public Annonce GetAnnonceById(int id)
        {
            return _AnnonceServices.GetAnnonceById(id); 
        }

        public async Task<List<Annonce>> GetAnnoncesByEmail(String email)
        {
            Membre membre = _MembreServices.GetMembreByEmail(email);
            if(membre == null)
            {
                throw new Exception("Member not found");
            }
            return await _AnnonceServices.GetAnnoncesByIdVendeur(membre.Id);
        }
    }
}
