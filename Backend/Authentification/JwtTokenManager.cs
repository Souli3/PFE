using Backend.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Backend.Models;

namespace Backend.Authentification
{
    public class JwtTokenManager : IJwtTokenManager
    {
        private readonly IConfiguration _configuration;
        private IDataContext _dataContext;
        private JwtSecurityTokenHandler _handler;
        public JwtTokenManager(IConfiguration configuration, IDataContext dataContext)
        {
            _configuration = configuration;
            _dataContext = dataContext;
            _handler = new JwtSecurityTokenHandler();
        }
        public string Authenticate(string email, string password)
        {
            Membre membre = _dataContext.Membres.FirstOrDefault(x => x.Email.Equals(email));
            if ( membre ==null) return null;

            String role = membre.Administrateur ? "Admin" : "User";

            var keyBytes = Encoding.ASCII.GetBytes(_configuration["JwtConfig:Key"]);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, email),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddDays(14),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string DecodeJWTToGetEmail(HttpRequest request)
        {
            String jwt = request.Headers[HeaderNames.Authorization];
            String jwtCleaned = jwt.Remove(0, 6).Trim();
            var token = _handler.ReadJwtToken(jwtCleaned);
            return token.Claims.First(claim => claim.Type == "nameid").Value;
        }
    }
}
