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
    }
    public class AnnonceLogic : IAnnonceLogic
    {
        private IAnnonceServices _AnnonceServices;
        public AnnonceLogic(IAnnonceServices AnnonceServices)
        {
            _AnnonceServices = AnnonceServices;
        }

        public Task<List<Annonce>> GetAllAnnonces()
        {
            return _AnnonceServices.GetAllAnnoncesAsync();
        }
    }
}
