using SnakServer.Db;
using SnakServer.Db.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakServer.Service
{
    /// <summary>
    /// A class dealing with all database operations in the application. Contains methods for creation as well as all DB operations the app permits.
    /// </summary>
    class DatabaseService
    {
        private SnakDbContext dbContext;

        /// <summary>
        /// The constructor creates a new DbContext class by connecting to a SQLite database bundled with the app.
        /// </summary>
        public DatabaseService()
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            dbContext = new SnakDbContext();
        }

        /// <summary>
        /// A getter method for the DbContext in case any element needed to do specific operations. Generally, should not be used in normal cases.
        /// </summary>
        /// <returns></returns>
        public SnakDbContext GetDbContext()
        {
            return dbContext;
        }

        /// <summary>
        /// A simple select for all level entities
        /// </summary>
        /// <returns>A List of Level objects</returns>
        public List<Level> GetAllLevels()
        {
            var query = from level in dbContext.Levels select level;
            return query.ToList();
        }

        /// <summary>
        /// A simple select for all player entities
        /// </summary>
        /// <returns>A list of Player objects</returns>
        public List<Player> GetAllPlayers()
        {
            var query = from player in dbContext.Players select player;
            return query.ToList();
        }

        /// <summary>
        /// A select for all scores reached on a specific level. Uses LINQ and converts the results to a List
        /// </summary>
        /// <param name="levelName">The name of level on which the scores were achieved</param>
        /// <returns>A list of Highscore objects</returns>
        public List<Highscore> GetAllScoresForLevel(String levelName)
        {
            var query = from level in dbContext.Levels
                        join score in dbContext.Scores on level.id equals score.level_id
                        join player in dbContext.Players on score.player_id equals player.id
                        where level.name == levelName
                        orderby score.score descending
                        select new Highscore { LevelName = level.name, Score = score.score, PlayerName = player.name };
            return query.ToList();
        }

        /// <summary>
        /// Returnsa all scores achieved by a single player
        /// </summary>
        /// <param name="playerName">The player's name string</param>
        /// <returns>A List of Player objects</returns>
        public List<Highscore> GetAllScoresForPlayer(String playerName)
        {
            var query = from level in dbContext.Levels
                        join score in dbContext.Scores on level.id equals score.level_id
                        join player in dbContext.Players on score.player_id equals player.id
                        where player.name == playerName
                        orderby level.name, score.score descending
                        select new Highscore { LevelName = level.name, Score = score.score, PlayerName = player.name };
            return query.ToList();
        }

        /// <summary>
        /// Returns all scores that were reached on all levels along with player names
        /// </summary>
        /// <returns>A List of Highscore objects</returns>
        public List<Highscore> GetAllLevelsWithScores()
        {
            var query = from level in dbContext.Levels
                        join score in dbContext.Scores on level.id equals score.level_id
                        join player in dbContext.Players on score.player_id equals player.id
                        orderby level.name, score.score descending
                        select new Highscore { LevelName = level.name, Score = score.score, PlayerName = player.name };
            return query.ToList();
        }

        /// <summary>
        /// Returns a list of all levels along with the top scores reached on them. Also returns levels without score.
        /// </summary>
        /// <returns>A List of Highscore objects, a single one for every level</returns>
        public List<Highscore> GetAllLevelsWithHighscores()
        {
            Dictionary<String, Highscore> highscores = new Dictionary<string, Highscore>();

            foreach (Level level in GetAllLevels())
            {
                highscores[level.name] = new Highscore { LevelName = level.name, PlayerName = "", Score = 0 };
            }

            foreach (Highscore score in GetAllLevelsWithScores())
            {
                if (highscores[score.LevelName].Score < score.Score)
                {
                    highscores[score.LevelName] = score;
                }
            }

            return highscores.Values.ToList();
        }

        /// <summary>
        /// Saves a new highscore to the DB
        /// </summary>
        /// <param name="highscore">A new highscore to be inserted</param>
        public void AddHighScore(Highscore highscore)
        {
            Player player;
            Level level;
            if (! dbContext.Players.Any(p => p.name == highscore.PlayerName))
            {
                player = new Player { name = highscore.PlayerName };
                dbContext.Players.Add(player);
                dbContext.SaveChanges();
            } else
            {
                player = (from p in dbContext.Players
                         where p.name == highscore.PlayerName
                         select p).Single();
            }
            if (! dbContext.Levels.Any(l => l.name == highscore.LevelName))
            {
                level = new Level { name = highscore.LevelName };
                dbContext.Levels.Add(level);
                dbContext.SaveChanges();
            } else
            {
                level = (from l in dbContext.Levels
                         where l.name == highscore.LevelName
                         select l).Single();
            }

            Score score = new Score { player_id = player.id, level_id = level.id, score = highscore.Score };
            dbContext.Scores.Add(score);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Resets all highscores.
        /// </summary>
        public void ClearHighScores()
        {
            dbContext.Scores.RemoveRange(dbContext.Scores);
            dbContext.SaveChanges();
        }
    }
}
