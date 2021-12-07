using Backend.Models;
using Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Logic
{
    public interface IRegisterLogic
    {
        public Task<Membre> Register(Membre membre);
    }
    public class RegisterLogic : IRegisterLogic
    {
        private IRegisterServices _registerServices;
        public RegisterLogic(IRegisterServices registerServices)
        {
            _registerServices = registerServices;
        }
        public async Task<Membre> Register(Membre membre)
        {
            Membre membreInscrit = await _registerServices.Register(membre);

            return membreInscrit;
        }
    }
}
