using Microsoft.EntityFrameworkCore;

namespace webapi.Data
{
    public class PostgreSQLContext : DbContext
    {
        public PostgreSQLContext(DbContextOptions<PostgreSQLContext> options) : base(options)
        { }
    }
}