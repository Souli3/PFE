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
        Task<Membre> UpdateMembre(Membre membre);
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
            Membre membre = _dataContext.Membres.Where(x=>x.Email.Equals(email)).FirstOrDefault();
            membre.Adresse = _dataContext.Adresses.Where(x=> x.Id==membre.Id).FirstOrDefault();
            return membre;            
        }

        public async Task<Membre> UpdateMembre(Membre membre)
        {
            var membreDB = _dataContext.Membres.FirstOrDefault(x => x.Email.Equals(membre.Email));
            membreDB.Campus_Id = membre.Campus_Id;
            membreDB.MotDePasse = membre.MotDePasse;
            //_dataContext.Membres.Update(membreDB);
            
            await _dataContext.SaveChangesAsync();
            return membreDB;
        }
    }
}
