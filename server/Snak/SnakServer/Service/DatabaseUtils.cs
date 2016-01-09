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
    /// <summary>
    /// A DbContext class containing DbSets of curently existing tables.
    /// </summary>
    public class SnakDbContext : DbContext
    {
        /// <summary>
        /// A set of all players
        /// </summary>
        public DbSet<Player> Players { get; set; }
        /// <summary>
        /// A set of all levels
        /// </summary>
        public DbSet<Level> Levels { get; set; }
        /// <summary>
        /// A set of all scores
        /// </summary>
        public DbSet<Score> Scores { get; set; }

        /// <summary>
        /// The constructor for DbContext. The connection string is generated dynamically.
        /// </summary>
        public SnakDbContext()
            : base(new SQLiteConnection()
            {
                ConnectionString =
                    new SQLiteConnectionStringBuilder() { ConnectionString = "Data Source=|DataDirectory|\\Db\\SnakServer.sqlite" }
                    .ConnectionString
            }, true)
        {

        }
    }

    /// <summary>
    /// A connection factory class required by EF
    /// </summary>
    public class SQLiteConnectionFactory : IDbConnectionFactory
    {
        /// <summary>
        /// Connection creation method
        /// </summary>
        /// <param name="nameOrConnectionString">db name or connectionString string</param>
        /// <returns>A DbConnection object</returns>
        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            return new SQLiteConnection(nameOrConnectionString);
        }
    }

    /// <summary>
    /// A configuration class defining the type of database used in the application.
    /// </summary>
    public class SQLiteConfiguration : DbConfiguration
    {
        /// <summary>
        /// The constructor with hardcoded values for SQLite DB connection
        /// </summary>
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
