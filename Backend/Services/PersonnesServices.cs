using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface IPersonnesServices
    {
        Personnes GetPersonnes(int id);
        Task<List<Personnes>> GetAllPersonnesAsync();
        Task AddPersonne(Personnes personnes);
    }
    public class PersonnesServices : IPersonnesServices
    {
        private IDataContext _dataContext;
        public PersonnesServices(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddPersonne(Personnes personnes)
        {
            
            _dataContext.Personnes.Add(personnes);
            await _dataContext.SaveChangesAsync();

        }

        public async Task<List<Personnes>> GetAllPersonnesAsync()
        {
            return await _dataContext.Personnes.ToListAsync();
        }

        public Personnes GetPersonnes(int id)
        {
            return _dataContext.Personnes.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
