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
        public Task<Personnes> Register(Personnes personnes);
    }
    public class RegisterLogic : IRegisterLogic
    {
        private IRegisterServices _registerServices;
        public RegisterLogic(IRegisterServices registerServices)
        {
            _registerServices = registerServices;
        }
        public async Task<Personnes> Register(Personnes personnes)
        {
            Personnes personneInscrite = await _registerServices.Register(personnes);

            return personneInscrite;
        }
    }
}
