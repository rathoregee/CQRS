using EventSourcing.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EventSourcing.Database
{
    public class PlayerContext : DbContext
    {
        //entities
        public DbSet<Player> Players { get; set; }

        public PlayerContext()
        {
        
        }
        public PlayerContext(IConfiguration configuration)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=PlayerDB;User Id=sa;Password=Kamran123@;TrustServerCertificate=True;");
        }
    }
}
