using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;
using System.Data.Entity;
using System.Reflection;
using System.Data.Entity.Core.Common;
using System.Data.SQLite.EF6;
using SnakServer.Db;

namespace SnakServer.Service
{
    public class SnakDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Score> Scores { get; set; }

        public SnakDbContext()
            : base(new SQLiteConnection()
            {
                ConnectionString =
                    new SQLiteConnectionStringBuilder() { ConnectionString = "Data Source=|DataDirectory|\\Db\\SnakServer.sqlite" } //"Data Source=C:\\dev\\pot\\server\\Snak\\SnakServer\\Db\\SnakServer.sqlite" }
                    .ConnectionString
            }, true)
        {

        }
    }

    public class SQLiteConnectionFactory : IDbConnectionFactory
    {
        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            return new SQLiteConnection(nameOrConnectionString);
        }
    }

    public class SQLiteConfiguration : DbConfiguration
    {
        public SQLiteConfiguration()
        {
            SetDefaultConnectionFactory(new SQLiteConnectionFactory());
            SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);
            SetProviderFactory("System.Data.SQLite.EF6", SQLiteProviderFactory.Instance);
            Type t = Type.GetType("System.Data.SQLite.EF6.SQLiteProviderServices, System.Data.SQLite.EF6");
            FieldInfo fi = t.GetField("Instance", BindingFlags.NonPublic | BindingFlags.Static);
            SetProviderServices("System.Data.SQLite", (DbProviderServices)fi.GetValue(null));
        }
    }
}
