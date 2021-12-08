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
        Task<Membre> Register(Membre membre);
    }

        public class RegisterServices : IRegisterServices
    {
        private IDataContext _dataContext;
        public RegisterServices(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Membre> Register(Membre membre)
        {
            Membre membre1 =  _dataContext.Membres.Add(membre).Entity;
            await _dataContext.SaveChangesAsync();
            return membre1;
        }
    }
}
