using Backend.Authentification;
using Backend.Logic;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Message>>> GetAllMessagesFromIdDiscussion(int id)
        {
            String email = _jwtTokenManager.DecodeJWTToGetEmail(Request);
            Membre valide = _DiscussionLogic.VerifyUserFromToken(id, email);
            if (valide == null) return Unauthorized("Vous ne faites pas partie de cette conversation");
            List<Message> messages = await _DiscussionLogic.GetAllMessagesFromIdDiscussion(id);
            return Ok(messages);
        }

        [HttpPost]
        public async Task<ActionResult<List<Message>>> PostMessageToDiscussion(Message message)
        {
            String email = _jwtTokenManager.DecodeJWTToGetEmail(Request);
            Membre valide = _DiscussionLogic.VerifyUserFromToken(message.Discussion_id, email);
            if (valide == null) return Unauthorized("Vous ne faites pas partie de cette conversation");
            Message messageDB = await _DiscussionLogic.PostMessageToDiscussion(message, valide);
            return Ok();
        }
    }
}
