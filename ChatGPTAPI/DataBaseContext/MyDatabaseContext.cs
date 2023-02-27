using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ChatGPTAPI.Controllers;
using ChatGPTAPI.DataModel;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace ChatGPTAPI.DataBaseContext
{
    public class MyDatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "BCPDb");
        }

        public virtual DbSet<UsuarioDataModel> Usuarios { get; set;}

    }
}
