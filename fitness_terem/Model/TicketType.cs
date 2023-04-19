using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;
using ForeignKeyAttribute = System.ComponentModel.DataAnnotations.Schema.ForeignKeyAttribute;

namespace fitness_terem.Model
{
    [SQLite.Table("ticketType")]
    public class TicketType
    {
        [PrimaryKey, AutoIncrement]
        public int Ticket_id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public int Nr_of_days_valid { get; set; }

        public int Nr_of_entry_valid { get; set; }

        public string Start_time_of_day { get; set; }

        public string End_time_of_day { get; set; }

        public bool Is_deleted { get; set; }

        [ForeignKey(nameof(Gym.Gym_id))]
        public int Gym_id { get; set; }
    }
}
