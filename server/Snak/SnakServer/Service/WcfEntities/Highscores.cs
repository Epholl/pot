using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace SnakServer.Service.WcfEntities
{
    [DataContract]
    public class Highscores
    {
        [DataMember]
        public List<Highscore> scores;
    }

    [DataContract]
    public class Highscore
    {
        [DataMember]
        public String playerName;
        [DataMember]
        public String levelName;
        [DataMember]
        public int score;
    }
}
