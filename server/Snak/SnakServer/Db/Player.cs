﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakServer.Db
{
    [Table("player")]
    public class Player
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
