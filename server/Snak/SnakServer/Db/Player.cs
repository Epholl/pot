using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakServer.Db
{
    /// <summary>
    /// A database entity representing a player. Currently only contains player's name.
    /// </summary>
    [Table("player")]
    public class Player
    {
        /// <summary>
        /// Database managed unique ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// An unique player's name set to any string.
        /// </summary>
        public string name { get; set; }
    }
}
