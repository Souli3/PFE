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

        public Task<List<Annonce>> GetAllAnnonces()
        {
            return _AnnonceServices.GetAllAnnoncesAsync();
        }

        public Task<List<Annonce>> GetAnnoncesByEmail(String email)
        {
            Membre membre = _MembreServices.GetMembreByEmail(email);
            if(membre == null)
            {
                throw new Exception("Member not found");
            }
            return _AnnonceServices.GetAnnoncesById(membre.Id);
        }
    }
}
