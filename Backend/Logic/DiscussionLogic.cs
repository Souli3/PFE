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
        Task<List<Discussion>> GetDiscussionsFromIdMembre(int id);
        Task<Discussion> PostDiscussion(int id1, int id2);
        bool VerifyUserFromIdAndToken(string email, int id);
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

        public async Task<List<Discussion>> GetDiscussionsFromIdMembre(int id)
        {
            return await _DiscussionService.GetDiscussionsFromIdMembre(id);
        }

        public async Task<Discussion> PostDiscussion(int id1, int id2)
        {
            Discussion discussionDB = _DiscussionService.GetDiscussionBetween2Members(id1, id2);
            if (discussionDB == null)
                discussionDB = await _DiscussionService.CreateDiscussion(id1, id2);
            return discussionDB;
        }

        public async Task<Message> PostMessageToDiscussion(Message message, Membre valide)
        {
            message.Envoyeur_id = valide.Id;
            message.Date_envoi = DateTime.Now;
            return await _DiscussionService.PostMessageToDiscussion(message);
        }

        public bool VerifyUserFromIdAndToken(string email, int id)
        {
            Membre testMembre = _MembreServices.GetMembre(id);
            if (testMembre.Email != email) return false;
            return true;
        }

        public Membre VerifyUserFromToken(int id, string email)
        {
            Discussion discussion = _DiscussionService.GetDiscussion(id);
            if (discussion == null) throw new Exception("Cette discussion n'existe pas");
            Membre test1 = _MembreServices.GetMembre(discussion.Membre1_id);
            if (test1.Email == email) return test1;
            Membre test2 = _MembreServices.GetMembre(discussion.Membre2_id);
            if (test2.Email == email) return test2;
            else throw new Exception("Vous ne faites pas partie de cette discussion");
        }
    }
}
