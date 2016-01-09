using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakServer.Db
{
    /// <summary>
    /// A simple entity representing level. In the future, the levels can be kept on the server instead of ingame.
    /// </summary>
    [Table("level")]
    public class Level
    {
        /// <summary>
        /// Database managed unique ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// An unique name representing the level
        /// </summary>
        public string name { get; set; }
    }
}
