using Backend.Data;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface IRegisterServices
    {
        Task<Personnes> Register(Personnes personnes);
    }

        public class RegisterServices : IRegisterServices
    {
        private IDataContext _dataContext;
        public RegisterServices(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Personnes> Register(Personnes personnes)
        {
            _dataContext.Personnes.Add(personnes);
            await _dataContext.SaveChangesAsync();
            return _dataContext.Personnes.Where(x => x.Name.Equals(personnes.Name) && x.MotDePasse.Equals(personnes.MotDePasse)).FirstOrDefault();
        }
    }
}
