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
    class ServerService : IServerService
    {
        private ServiceHost serviceHost;

        public ServerService()
        {
            Uri baseAddress = new Uri("http://localhost:8000/Snak/");
            serviceHost = new ServiceHost(typeof(ServerService), baseAddress);
            SL.Register(this);
        }

        public void StartService()
        {
            try
            {
                serviceHost.AddServiceEndpoint(typeof(IServerService), new WSHttpBinding(), "ServerService");
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

        public void StopService()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }
        }

        public int GetSampleData(int input)
        {
            return input +1;
        }

        public void SaveHighscore(string playerName, string levelName, int score)
        {
            var db = (DatabaseService)SL.Get(typeof(DatabaseService));
            db.AddHighScore(new Db.Util.Highscore { PlayerName = playerName, LevelName = levelName, Score = score });
        }

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
