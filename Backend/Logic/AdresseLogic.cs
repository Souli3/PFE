using Backend.Models;
using Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Logic
{
    public interface IAdresseLogic
    {
        Task<List<Adresse>> GetAllAdresses();
    }
    public class AdresseLogic : IAdresseLogic
    {
        private IAdresseService _AdresseService;
        public AdresseLogic(IAdresseService AdresseServices)
        {
            _AdresseService = AdresseServices;
        }

        public async Task<List<Adresse>> GetAllAdresses()
        {
            return await _AdresseService.GetAllAdresses();
        }
    }
}
