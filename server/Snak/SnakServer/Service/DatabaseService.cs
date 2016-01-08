using SnakServer.Db.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakServer.Service
{
    class DatabaseService
    {
        private SnakDbContext dbContext;

        public DatabaseService()
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            dbContext = new SnakDbContext();
        }

        public SnakDbContext GetDbContext()
        {
            return dbContext;
        }

        public List<Highscore> GetAllLevelsWithHighScores()
        {
            var query = from level in dbContext.Levels
                             join score in dbContext.Scores on level.id equals score.level_id
                             join player in dbContext.Players on score.player_id equals player.id
                             select new Highscore { LevelName = level.name, Score = score.score, PlayerName = player.name };
            return query.ToList();
        }
    }
}
