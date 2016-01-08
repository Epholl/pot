using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakServer.Db
{
    [Table("level")]
    public class Level
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
