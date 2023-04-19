using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace fitness_terem.Model
{
    [Table ("client")]
    public class Client
    {

        [PrimaryKey, AutoIncrement]
        public int Client_id { get; set; }

        public string Name { get; set; }

        [Unique]
        public string Phone_nr { get; set; }

        [MinLength(10)]
        public string Password { get; set; }

        [Unique]
        public string Email { get; set; }

        [DefaultValue(false)]
        public bool Is_deleted { get; set; }

        [SQLite.MaxLength(50)]
        public string Address { get; set; }

        public DateTime Inserted_date { get; set; }

        public string Description { get; set; }

    }
}
