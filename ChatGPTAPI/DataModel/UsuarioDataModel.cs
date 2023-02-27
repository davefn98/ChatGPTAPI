using System.ComponentModel.DataAnnotations;

namespace ChatGPTAPI.DataModel
{
    public class UsuarioDataModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? User { get; set; }
        public string? Password { get; set; }
        public string? Rol { get; set; }
        public string? RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
