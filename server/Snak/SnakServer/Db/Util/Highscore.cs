using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakServer.Db.Util
{
    /// <summary>
    /// Utility class that does not exist as an entity, but is a result of a database high score selection operation.
    /// </summary>
    public class Highscore
    {
        /// <summary>
        /// Level, on which the score was achieved
        /// </summary>
        public String LevelName { get; set; }
        /// <summary>
        /// The score itself
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// Player name. No login mechanism exists at present moment
        /// </summary>
        public String PlayerName { get; set; }

        /// <summary>
        /// Conversion utility method to convert to WCF compatible mirror entity.
        /// </summary>
        /// <returns></returns>
        public SnakServer.Service.WcfEntities.Highscore ToWcfHighscore()
        {
            return new Service.WcfEntities.Highscore { levelName = LevelName, playerName = PlayerName, score = Score };
        }
    }
}
