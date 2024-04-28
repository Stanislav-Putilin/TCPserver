using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DbDataModels;

namespace ServerTCP
{
    public class ServerContext : DbContext
    {
        public DbSet<User> Users { get; set; } = default!;

        public DbSet<UserMessage> UserMessages { get; set; } = default!;

        public ServerContext() : base() { }

        public ServerContext(DbContextOptions<ServerContext> options) : base(options)
        {

        }
    }
}