using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChatGPTAPI.DataModel;
using ChatGPTAPI.Models;
using ChatGPTAPI.DataBaseContext;
using Microsoft.EntityFrameworkCore;

namespace ChatGPTAPI.Repository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        //Dictionary<string, string> UsersRecords = new Dictionary<string, string> {
        //    { "user1","password1" }
        //};

        private readonly IConfiguration iconfiguration;

        public JWTManagerRepository(IConfiguration iconfiguration) {
            iconfiguration = iconfiguration;
        }

        public TokensDataModel Authenticate(LoginModel login, UsuarioDataModel users)
        {
            if (!UserGlobal.(x => x.Key == users.Name && x.Value == users.Password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, users.User)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokensDataModel { Token = tokenHandler.WriteToken(token) };
        }

    }
}
