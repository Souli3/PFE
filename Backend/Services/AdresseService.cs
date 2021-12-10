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
    }
}
