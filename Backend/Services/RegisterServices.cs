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
            membre.Campus_Id = 1;
            _dataContext.Membres.Add(membre);
            await _dataContext.SaveChangesAsync();
            return _dataContext.Membres.Where(x => x.Email.Equals(membre.Email) && x.MotDePasse.Equals(membre.MotDePasse)).FirstOrDefault();
        }
    }
}
