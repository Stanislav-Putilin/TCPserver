using DbDataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataBaseContext
{
    public class MyServerContextFactory : IDesignTimeDbContextFactory<ServerContext>
    {
        public ServerContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ServerContext> contextOptionsBuilder = new DbContextOptionsBuilder<ServerContext>();
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configurationBuilder.AddJsonFile("appsettings.json");
            var configuration = configurationBuilder.Build();
            string connStr = configuration.GetConnectionString("myDbTournament");
            contextOptionsBuilder.UseSqlServer(connStr);
            var options = contextOptionsBuilder.Options;
            ServerContext context = new ServerContext(options);
            return context;
        }
    }
}
