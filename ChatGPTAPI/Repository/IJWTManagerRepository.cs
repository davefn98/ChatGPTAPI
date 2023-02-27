using Newtonsoft.Json.Linq;
using ChatGPTAPI.DataModel;
using ChatGPTAPI.Models;
using ChatGPTAPI.DataBaseContext;

namespace ChatGPTAPI.Repository
{
    public interface IJWTManagerRepository
    {
        TokensDataModel Authenticate(LoginModel login, UsuarioDataModel users);
    }
}
