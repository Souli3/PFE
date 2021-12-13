using Backend.Models;
using Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Logic
{
    public interface IDiscussionLogic
    {
        Task<List<Message>> GetAllMessagesFromIdDiscussion(int id);
        Membre VerifyUserFromToken(int id, string email);
        Task<Message> PostMessageToDiscussion(Message message, Membre valide);
    }
    public class DiscussionLogic : IDiscussionLogic
    {
        private IDiscussionService _DiscussionService;
        private IMembreServices _MembreServices;
        public DiscussionLogic(IDiscussionService DiscussionService, IMembreServices MembreServices)
        {
            _DiscussionService = DiscussionService;
            _MembreServices = MembreServices;
        }

        public async Task<List<Message>> GetAllMessagesFromIdDiscussion(int id)
        {
            return await _DiscussionService.GetAllMessagesFromIdDiscussion(id);
        }

        public async Task<Message> PostMessageToDiscussion(Message message, Membre valide)
        {
            message.Envoyeur_id = valide.Id;
            message.Date_envoi = DateTime.Now;
            return await _DiscussionService.PostMessageToDiscussion(message);
        }

        public Membre VerifyUserFromToken(int id, string email)
        {
            Discussion discussion = _DiscussionService.GetDiscussion(id);
            Membre test1 = _MembreServices.GetMembre(discussion.Membre1_id);
            Membre test2 = _MembreServices.GetMembre(discussion.Membre2_id);
            if (test1.Email == email) return test1;
            else if (test2.Email == email) return test2;
            else return null;
        }
    }
}
