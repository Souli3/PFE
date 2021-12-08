using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Authentification
{
    public interface IJwtTokenManager
    {
        string Authenticate(string userName, string password);
        string DecodeJWTToGetEmail(HttpRequest request);
    }
}
