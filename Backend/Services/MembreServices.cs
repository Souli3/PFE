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
        Membre GetMembreByEmail(string email);
        Task<Membre> UpdateMembre(Membre membre);
        Task<Membre> BanMembre(Membre membre, int time);
        Task<Membre> PutAdmin(int id);
    }
    public class MembreServices : IMembreServices
    {
        private IDataContext _dataContext;
        public MembreServices(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Membre> BanMembre(Membre membre, int time)
        {
            DateTime bannedUntil;
            if (time == -1)
                bannedUntil = DateTime.MaxValue;
            else
                bannedUntil = DateTime.Now.AddDays(time);
            Membre membreDB = _dataContext.Membres.FirstOrDefault(x => x.Email.Equals(membre.Email));

            //Time == 0 means UNBAN
            if(time == 0)
            {
                membreDB.Banni = DateTime.Now;
            }
            else
            {
                //Verify if member is already banned and if new Date Ban is lower than current Date Ban
                if (membreDB.Banni != null && membreDB.Banni > DateTime.Now && bannedUntil < membreDB.Banni)
                    return null;

                membreDB.Banni = bannedUntil;
            }

            await _dataContext.SaveChangesAsync();
            return membreDB;
        }

        public async Task<List<Membre>> GetAllMembresAsync()
        {
            return await _dataContext.Membres.OrderBy(x => x.Id).ToListAsync();
        }

        public Membre GetMembre(int id)
        {
            return _dataContext.Membres.Where(x => x.Id == id).FirstOrDefault();
        }
        public Membre GetMembreByEmail(string email)
        {
            Membre membre = _dataContext.Membres.Where(x=>x.Email.Equals(email)).FirstOrDefault();
            if(membre == null) return null;
            membre.Adresse = _dataContext.Adresses.Where(x=> x.Id==membre.Campus_Id).FirstOrDefault();
            return membre;            
        }

        public async Task<Membre> PutAdmin(int id)
        {
            Membre membreDB = this.GetMembre(id);
            membreDB.Administrateur = true;

            await _dataContext.SaveChangesAsync();
            return membreDB;
        }

        public async Task<Membre> UpdateMembre(Membre membre)
        {
            var membreDB = _dataContext.Membres.FirstOrDefault(x => x.Email.Equals(membre.Email));
            membreDB.Campus_Id = membre.Campus_Id;
            membreDB.MotDePasse = membre.MotDePasse;
            
            await _dataContext.SaveChangesAsync();
            membreDB.Adresse =  _dataContext.Adresses.Where(x => x.Id == membreDB.Campus_Id).FirstOrDefault();
            return membreDB;
        }
    }
}
