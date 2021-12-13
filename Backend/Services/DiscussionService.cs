using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface IDiscussionService
    {
        Task<List<Message>> GetAllMessagesFromIdDiscussion(int id);
        Discussion GetDiscussion(int id);
        Task<Message> PostMessageToDiscussion(Message message);
        Task<List<Discussion>> GetDiscussionsFromIdMembre(int id);
        Discussion GetDiscussionBetween2Members(int id1, int id2);
        Task<Discussion> CreateDiscussion(int id1, int id2);
    }
    public class DiscussionService : IDiscussionService
    {
        private IDataContext _dataContext;
        public DiscussionService(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Discussion> CreateDiscussion(int id1, int id2)
        {
            Discussion newDiscussion = new Discussion();
            newDiscussion.Membre1_id = id1;
            newDiscussion.Membre2_id = id2;
            Discussion discussionDB = _dataContext.Discussions.Add(newDiscussion).Entity;
            await _dataContext.SaveChangesAsync();
            return discussionDB;
        }

        public async Task<List<Message>> GetAllMessagesFromIdDiscussion(int id)
        {
            return await _dataContext.Messages.Where(message => message.Discussion_id == id).ToListAsync();
        }

        public Discussion GetDiscussion(int id)
        {
            return _dataContext.Discussions.Where(discussion => discussion.Id == id).FirstOrDefault();
        }

        public Discussion GetDiscussionBetween2Members(int id1, int id2)
        {
            Discussion discussionDB;
            discussionDB = _dataContext.Discussions.Where(discussion => discussion.Membre1_id == id1 && discussion.Membre2_id == id2).FirstOrDefault();
            if (discussionDB != null) return discussionDB;
            discussionDB = _dataContext.Discussions.Where(discussion => discussion.Membre1_id == id2 && discussion.Membre2_id == id1).FirstOrDefault();
            if (discussionDB != null) return discussionDB;
            return null;
        }

        public async Task<List<Discussion>> GetDiscussionsFromIdMembre(int id)
        {
            List<Discussion> list1 = await _dataContext.Discussions.Where(discussion => discussion.Membre1_id == id).ToListAsync();
            List<Discussion> list2 = await _dataContext.Discussions.Where(discussion => discussion.Membre2_id == id).ToListAsync();
            return list1.Union(list2).OrderBy(discussion => discussion.Id).ToList();
        }

        public async Task<Message> PostMessageToDiscussion(Message message)
        {
            Message messageDB = _dataContext.Messages.Add(message).Entity;
            await _dataContext.SaveChangesAsync();
            return messageDB;
        }
    }
}
