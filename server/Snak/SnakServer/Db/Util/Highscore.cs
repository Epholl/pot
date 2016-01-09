using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakServer.Db.Util
{
    public class Highscore
    {
        public String LevelName { get; set; }
        public int Score { get; set; }
        public String PlayerName { get; set; }

        public SnakServer.Service.WcfEntities.Highscore ToWcfHighscore()
        {
            return new Service.WcfEntities.Highscore { levelName = LevelName, playerName = PlayerName, score = Score };
        }
    }
}
