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
        Task AddMembre(Membre Membres);
        Task<Membre> UpdateMember(Membre membre);


    }
    public class MemberLogic : IMembresLogic
    {
        private IMembreServices _MembreServices;
        public MemberLogic(IMembreServices MembreServices)
        {
            _MembreServices = MembreServices;
        }
        public async Task AddMembre(Membre Membres)
        {
            await _MembreServices.AddMembre(Membres);
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
