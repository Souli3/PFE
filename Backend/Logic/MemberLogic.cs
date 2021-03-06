using Backend.Models;
using Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Logic
{
    public interface IMembresLogic
    {
        Membre GetMembre(string email);
        Task<List<Membre>> GetAllMembres();
        Task<Membre> UpdateMember(Membre membre);
        Task<Membre> BanMembre(int id, int time);
        Membre GetMembreById(int id);
        Task<Membre> PutAdmin(int id);
    }
    public class MemberLogic : IMembresLogic
    {
        private IMembreServices _MembreServices;
        public MemberLogic(IMembreServices MembreServices)
        {
            _MembreServices = MembreServices;
        }
        

        public async Task<Membre> BanMembre(int id, int time)
        {
            Membre membre = _MembreServices.GetMembre(id);
            if (membre == null)
                throw new Exception("Membre non trouvé");
            Membre membreBanni = await _MembreServices.BanMembre(membre, time);
            if(membreBanni == null)
                throw new Exception("Ce membre est déjà banni pour une durée supérieure");
            return membreBanni;
        }

        public async Task<List<Membre>> GetAllMembres()
        {
            
            List<Membre> membres = await _MembreServices.GetAllMembresAsync();
            return membres;
        }

        public Membre GetMembre(string email)
        {
            return _MembreServices.GetMembreByEmail(email);
        }

        public Membre GetMembreById(int id)
        {
            return _MembreServices.GetMembre(id);
        }

        public async Task<Membre> PutAdmin(int id)
        {
            Membre membre = await _MembreServices.PutAdmin(id);
            if (membre == null) throw new Exception("Membre non trouvé");
            return membre;
        }

        public async Task<Membre> UpdateMember(Membre membre)
        {
            Membre membreDB = _MembreServices.GetMembreByEmail(membre.Email);
            if(membre.Campus_Id!=null && membre.Campus_Id!= 0) { 
                if(membre.Campus_Id!=membreDB.Campus_Id) membreDB.Campus_Id = membre.Campus_Id;
            }
            if (membre.MotDePasse!=null && membre.MotDePasse != "") { 
                if(!BCrypt.Net.BCrypt.Verify(membre.MotDePasse, membreDB.MotDePasse)) membreDB.MotDePasse= BCrypt.Net.BCrypt.HashPassword(membre.MotDePasse) ;
            }
            return await _MembreServices.UpdateMembre(membreDB);
        }
    }
}
