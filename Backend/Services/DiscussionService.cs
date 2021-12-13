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
    }
    public class DiscussionService : IDiscussionService
    {
        private IDataContext _dataContext;
        public DiscussionService(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<List<Message>> GetAllMessagesFromIdDiscussion(int id)
        {
            return await _dataContext.Messages.Where(message => message.Discussion_id == id).ToListAsync();
        }

        public Discussion GetDiscussion(int id)
        {
            return _dataContext.Discussions.Where(discussion => discussion.Id == id).FirstOrDefault();
        }

        public async Task<Message> PostMessageToDiscussion(Message message)
        {
            Message messageDB = _dataContext.Messages.Add(message).Entity;
            await _dataContext.SaveChangesAsync();
            return messageDB;
        }
    }
}
