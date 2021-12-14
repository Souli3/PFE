using Backend.Models;
using Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Backend.Logic
{
    public interface IRegisterLogic
    {
        public Task<Membre> Register(Membre membre);
        Membre Login(Membre membre);
        Task<Membre> Validate(int id);
    }
    public class RegisterLogic : IRegisterLogic
    {
        private IRegisterServices _registerServices;
        private IMembreServices _membreServices;
        public RegisterLogic(IRegisterServices registerServices, IMembreServices membreServices)
        {
            _registerServices = registerServices;
            _membreServices = membreServices;
        }
        public Membre Login(Membre membre)
        {
            return _membreServices.GetMembreByEmail(membre.Email);
        }
        public async Task<Membre> Register(Membre membre)
        {
            if (_membreServices.GetMembreByEmail(membre.Email) != null) return null;
            Membre membreInscrit = await _registerServices.Register(membre);
            return membreInscrit;
        }

        public async Task<Membre> Validate(int id)
        {
            Membre membreDB = _membreServices.GetMembre(id);
            if (membreDB == null) return null;
            membreDB.Valide = true;
            return await _membreServices.UpdateMembre(membreDB);
        }
    }
}
