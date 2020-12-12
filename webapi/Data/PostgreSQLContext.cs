using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Data
{
    public class PostgreSQLContext : DbContext
    {
        public PostgreSQLContext(DbContextOptions<PostgreSQLContext> options) : base(options)
        { }

        public DbSet<Usuario> Usuarios { get; set; }

    }
}