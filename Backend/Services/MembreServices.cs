using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface IMembreServices
    {
        Membre GetMembre(int id);
        Task<List<Membre>> GetAllMembresAsync();
        Task AddMembre(Membre Membre);
        Membre GetMembreByEmail(string email);
    }
    public class MembreServices : IMembreServices
    {
        private IDataContext _dataContext;
        public MembreServices(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddMembre(Membre membre)
        {
            
            _dataContext.Membres.Add(membre);
            await _dataContext.SaveChangesAsync();

        }

        public async Task<List<Membre>> GetAllMembresAsync()
        {
            return await _dataContext.Membres.ToListAsync();
        }

        public Membre GetMembre(int id)
        {
            return _dataContext.Membres.Where(x => x.Id == id).FirstOrDefault();
        }
        public Membre GetMembreByEmail(string email)
        {
            return _dataContext.Membres.Where(x=>x.Email.Equals(email)).FirstOrDefault();
        }
    }
}
