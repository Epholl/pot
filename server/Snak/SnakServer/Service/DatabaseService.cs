﻿using SnakServer.Db;
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

        public List<Level> GetAllLevels()
        {
            var query = from level in dbContext.Levels select level;
            return query.ToList();
        }

        public List<Player> GetAllPlayers()
        {
            var query = from player in dbContext.Players select player;
            return query.ToList();
        }

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

        public List<Highscore> GetAllLevelsWithScores()
        {
            var query = from level in dbContext.Levels
                        join score in dbContext.Scores on level.id equals score.level_id
                        join player in dbContext.Players on score.player_id equals player.id
                        orderby level.name, score.score descending
                        select new Highscore { LevelName = level.name, Score = score.score, PlayerName = player.name };
            return query.ToList();
        }

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

        public void ClearHighScores()
        {
            dbContext.Scores.RemoveRange(dbContext.Scores);
            dbContext.SaveChanges();
        }
    }
}
