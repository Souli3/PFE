using Backend.Models;
using Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Logic
{
    public interface IPersonnesLogic
    {
        Personnes GetPersonnes(int id);
        Task<List<Personnes>> GetAllPersonnes();
        Task AddPersonne(Personnes personnes);

    }
    public class PersonnesLogic : IPersonnesLogic
    {
        private IPersonnesServices _personnesServices;
        public PersonnesLogic(IPersonnesServices personnesServices)
        {
            _personnesServices = personnesServices;
        }
        public async Task AddPersonne(Personnes personnes)
        {
            await _personnesServices.AddPersonne(personnes);
        }

        public async Task<List<Personnes>> GetAllPersonnes()
        {
            
            List<Personnes> pers = await _personnesServices.GetAllPersonnesAsync();
            return pers;
        }

        public Personnes GetPersonnes(int id)
        {
            return _personnesServices.GetPersonnes(id);
        }
    }
}
