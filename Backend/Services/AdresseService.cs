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
    }
    public class AdresseService : IAdresseService
    {
        private IDataContext _dataContext;
        public AdresseService(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<List<Adresse>> GetAllAdresses()
        {
            return await _dataContext.Adresses.ToListAsync();
        }
    }
}
