using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace SnakServer.Service.WcfEntities
{
    /// <summary>
    /// A WCF entity class that can be sent to the client application to display highscores in various ways.
    /// </summary>
    [DataContract]
    public class Highscores
    {
        /// <summary>
        /// A list for all concerned highscores. Might represent all highscores for a level or all githscores of a player...
        /// </summary>
        [DataMember]
        public List<Highscore> scores;
    }

    /// <summary>
    /// An internal plain old c# object class representing a single highscore. Annotated to be used in WCF.
    /// </summary>
    [DataContract]
    public class Highscore
    {
        /// <summary>
        /// The name of the player who achieved the highscore
        /// </summary>
        [DataMember]
        public String playerName;
        /// <summary>
        /// The name of the level on which the score was achieved
        /// </summary>
        [DataMember]
        public String levelName;
        /// <summary>
        /// The score itself as an integer value
        /// </summary>
        [DataMember]
        public int score;
    }
}
