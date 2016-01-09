using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace SnakServer.Service
{
    [ServiceContract]
    interface IServerService
    {

        [OperationContract]
        int GetSampleData(int input);

        [OperationContract]
        void SaveHighscore(String playerName, String levelName, int score);

        [OperationContract]
        WcfEntities.Highscores getHighscoresForLevel(String levelName);

        [OperationContract]
        WcfEntities.Highscores getHighscoresForPlayer(String playerName);
    }
}
