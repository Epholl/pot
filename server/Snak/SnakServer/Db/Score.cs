using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakServer.Db
{
    [Table("score")]
    public class Score
    {
        public int id { get; set; }
        public int player_id { get; set; }
        public int level_id { get; set; }
        public int score { get; set; }
    }
}
