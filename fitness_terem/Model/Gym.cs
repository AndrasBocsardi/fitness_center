using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
namespace fitness_terem.Model
{
    [Table ("gym")]
    public class Gym
    {
        [PrimaryKey, AutoIncrement]
        public int Gym_id { get; set; }

        public string Name { get; set; }

        [DefaultValue (false)]
        public bool Is_deleted { get; set; }
    }
}
