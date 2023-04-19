using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForeignKeyAttribute = System.ComponentModel.DataAnnotations.Schema.ForeignKeyAttribute;
using SQLite;

namespace fitness_terem.Model
{
    [Table ("entry")]
    public class Entry
    {
        [PrimaryKey, AutoIncrement]
        public int Entry_id { get; set; }

        [ForeignKey(nameof(Client.Client_id))]
        public int Client_id { get; set; }

        public int Client_ticket_id { get; set; }

        public DateTime Date { get; set; }        

        [ForeignKey(nameof(Gym.Gym_id))]
        public int Gym_id { get; set; } 




    }
}
