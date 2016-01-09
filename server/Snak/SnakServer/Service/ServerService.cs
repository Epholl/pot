using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using SnakServer.Service.WcfEntities;

namespace SnakServer.Service
{
    /// <summary>
    /// The implementation of the WCF interface.
    /// </summary>
    class ServerService : IServerService
    {
        private ServiceHost serviceHost;

        /// <summary>
        /// The service runs on a hardcoded localhost URI on port 8000
        /// </summary>
        public ServerService()
        {
            Uri baseAddress = new Uri("http://localhost:8000/Snak/");
            serviceHost = new ServiceHost(typeof(ServerService), baseAddress);
            SL.Register(this);
        }

        /// <summary>
        /// The method to start self-hosting the service in the application
        /// The endpoint is named ServerService and runs using basicHttpBinding
        /// </summary>
        public void StartService()
        {
            try
            {
                serviceHost.AddServiceEndpoint(typeof(IServerService), new BasicHttpBinding(), "ServerService");
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                serviceHost.Description.Behaviors.Add(smb);
                
                serviceHost.Open();
            }
            catch (CommunicationException ce)
            {
                serviceHost.Abort();
            }
        }

        /// <summary>
        /// The service is stopped when the server application is clossed to prevent leaks.
        /// </summary>
        public void StopService()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }
        }

        /// <summary>
        /// A testing method that increments a parameter and returns it
        /// </summary>
        /// <param name="input">Any int</param>
        /// <returns>The parameter incremented by one</returns>
        public int GetSampleData(int input)
        {
            return input +1;
        }

        /// <summary>
        /// The method saves a highscore to the database.
        /// </summary>
        /// <param name="playerName">Name of the player who achieved the highscore</param>
        /// <param name="levelName">Name of the level on which the score was achieved</param>
        /// <param name="score">The score itself as an integer</param>
        public void SaveHighscore(string playerName, string levelName, int score)
        {
            var db = (DatabaseService)SL.Get(typeof(DatabaseService));
            db.AddHighScore(new Db.Util.Highscore { PlayerName = playerName, LevelName = levelName, Score = score });
        }

        /// <summary>
        /// Returns all highscores reached on a level
        /// </summary>
        /// <param name="levelName">The level's name</param>
        /// <returns>A List of WCF Highscore objects</returns>
        public Highscores getHighscoresForLevel(string levelName)
        {
            var db = (DatabaseService)SL.Get(typeof(DatabaseService));
            List<Db.Util.Highscore> highscores = db.GetAllScoresForLevel(levelName);
            List<Highscore> scores = new List<Highscore>();

            foreach (Db.Util.Highscore highscore in highscores)
            {
                scores.Add(highscore.ToWcfHighscore());
            }

            return new Highscores { scores = scores };
        }

        /// <summary>
        /// Returns all highscores reached by a player on all levels
        /// </summary>
        /// <param name="playerrName">The player's name</param>
        /// <returns>A List of WCF Highscore objects</returns>
        public Highscores getHighscoresForPlayer(string playerName)
        {
            var db = (DatabaseService)SL.Get(typeof(DatabaseService));
            List<Db.Util.Highscore> highscores = db.GetAllScoresForPlayer(playerName);

            List<Highscore> scores = new List<Highscore>();

            foreach (Db.Util.Highscore highscore in highscores)
            {
                scores.Add(highscore.ToWcfHighscore());
            }

            return new Highscores { scores = scores };
        }
    }
}
