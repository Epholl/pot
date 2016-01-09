using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakServer.Db
{
    /// <summary>
    /// An M:N relation entity. Each score defines one play of a level by a player with a score that was achieved during play.
    /// </summary>
    [Table("score")]
    public class Score
    {
        /// <summary>
        /// A database managed unique identifier
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// The database ID of a player who reached this highscore
        /// </summary>
        public int player_id { get; set; }
        /// <summary>
        /// The database ID of the level, on which the score was achieved
        /// </summary>
        public int level_id { get; set; }
        /// <summary>
        /// The score itself as an integer
        /// </summary>
        public int score { get; set; }
    }
}
