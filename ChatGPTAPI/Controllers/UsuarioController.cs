using ChatGPTAPI.DataBaseContext;
using ChatGPTAPI.DataModel;
using ChatGPTAPI.Models;
using ChatGPTAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using static ChatGPTAPI.DataBaseContext.MyDatabaseContext;
using static System.Net.Mime.MediaTypeNames;

namespace ChatGPTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly MyDatabaseContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UsuarioController> _logger;
        private readonly IJWTManagerRepository _jWTManager;

        public UsuarioController(IJWTManagerRepository jWTManager, IConfiguration configuration, MyDatabaseContext dbcontext, ILogger<UsuarioController> logger)
        {
            _configuration = configuration;
            _dbContext = dbcontext;
            _logger = logger;
            this._jWTManager = jWTManager;
        }

        [HttpPost]
        [Route("Registrar")]
        public async Task<ActionResult> Registrar(UsuarioModel request)
        {
            var existeUsuario = await _dbContext.Usuarios.AnyAsync(x => x.User == request.User);

            UsuarioDataModel UserGlobal = new UsuarioDataModel();

            if (existeUsuario)
            {
                return BadRequest($"El Usuario con Id : {request.User} " + " No Existe");
            }
            UsuarioDataModel user = new UsuarioDataModel();
            UserGlobal.Id = await BuscaMaximo();
            UserGlobal.Name = request.Name;
            UserGlobal.LastName = request.LastName;
            UserGlobal.User = request.Name.Substring(0, 1) + request.LastName;
            UserGlobal.Password = request.Password;
            UserGlobal.Rol = request.Rol;

            _dbContext.Usuarios.Add(UserGlobal);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(LoginModel request)
        {
            var user = Authenticate(request);
            
            return NotFound("Usuario no encontrado");
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate(LoginModel login)
        {
            UsuarioDataModel users = new UsuarioDataModel();
            var token = _jWTManager.Authenticate(login);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [HttpGet]
        [Route("ObtenerMaximoId")]
        public async Task<int> BuscaMaximo()
        {
            int result = 0;
            var latestStudentRecord = _dbContext.Usuarios.OrderByDescending(x => x.Id).FirstOrDefault();

            if (latestStudentRecord == null)
            {
                result = 1;
            }
            else
            {
                result = 1 + latestStudentRecord.Id;
            }
            return result;
        }

        [HttpGet]
        [Route("ListaUsuarios")]
        //[Authorize(Roles = ("Admin"))]
        public async Task<ActionResult<List<UsuarioDataModel>>> ListaUsuarios()
        {
            var result = _dbContext.Usuarios.ToList();

            UsuarioDataModel model = new UsuarioDataModel();

            if (result.Count > 0)
            {
                ////UsuarioDataModel? users = await _dbContext.Usuarios.FirstOrDefaultAsync(
                ////                                        x => x.User == loginModel.User);
                //var users = await _dbContext.Usuarios.FirstOrDefaultAsync(
                //                        x => x.User == loginModel.User);

                //if (currentUser.Rol == "Admin")
                //{
                return Ok(result);
                //}
                //else
                //{
                //    return Unauthorized();
                //}
            }
            else
            {
                return NotFound();
            }
        }
    }
}