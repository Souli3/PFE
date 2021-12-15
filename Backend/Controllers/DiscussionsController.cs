using Backend.Authentification;
using Backend.Logic;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PusherServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DiscussionsController : ControllerBase
    {
        private readonly IJwtTokenManager _jwtTokenManager;
        private IDiscussionLogic _DiscussionLogic;
        public DiscussionsController(IDiscussionLogic DiscussionLogic, IJwtTokenManager jwtTokenManager)
        {
            _DiscussionLogic = DiscussionLogic;
            _jwtTokenManager = jwtTokenManager;
        }

        [HttpGet("messages/{id}")]
        public async Task<ActionResult<List<Message>>> GetAllMessagesFromIdDiscussion(int id)
        {
            String email = _jwtTokenManager.DecodeJWTToGetEmail(Request);
            Membre valide;
            try
            {
                valide = _DiscussionLogic.VerifyUserFromToken(id, email);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
            List<Message> messages = await _DiscussionLogic.GetAllMessagesFromIdDiscussion(id);
            return Ok(messages);
        }

        [HttpPost("messages")]
        public async Task<ActionResult<List<Message>>> PostMessageToDiscussion(Message message)
        {
            String email = _jwtTokenManager.DecodeJWTToGetEmail(Request);
            Membre valide;
            try
            {
                valide = _DiscussionLogic.VerifyUserFromToken(message.Discussion_id, email);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
            Message messageDB = await _DiscussionLogic.PostMessageToDiscussion(message, valide);
            var options = new PusherOptions();
            options.Cluster = "eu";

            var pusher = new Pusher("1315901", "93dc2573318267ee5994", "4f35b2a34b5a87cecc75", options);
            var result = await pusher.TriggerAsync("chat", "message", messageDB);
            return Ok(messageDB);
        }

        [HttpGet("membres/{id}")]
        public async Task<ActionResult<List<Message>>> GetDiscussionsFromIdMembre(int id)
        {
            List<Discussion> discussions = await _DiscussionLogic.GetDiscussionsFromIdMembre(id);
            if(discussions == null) return NotFound("Discussion inexistante !");
            return Ok(discussions);
        }

        [HttpPost("membres/{id1}/{id2}")]
        public async Task<ActionResult<List<Message>>> PostDiscussion(int id1, int id2)
        {
            String email = _jwtTokenManager.DecodeJWTToGetEmail(Request);
            bool test1 = _DiscussionLogic.VerifyUserFromIdAndToken(email, id1);
            bool test2 = _DiscussionLogic.VerifyUserFromIdAndToken(email, id2);
            if (!test1 && !test2) return Unauthorized("Vous ne pouvez pas créer cette discussion pusique vous n'en faites pas partie");
           
            Discussion discussionDB = await _DiscussionLogic.PostDiscussion(id1, id2);
            return Ok(discussionDB);
        }
    }
}
