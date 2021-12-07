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
        Membre GetMembre(int id);
        Task<List<Membre>> GetAllMembres();
        Task AddMembre(Membre Membres);

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

        public Membre GetMembre(int id)
        {
            return _MembreServices.GetMembre(id);
        }
    }
}
