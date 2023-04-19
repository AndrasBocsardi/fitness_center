using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForeignKeyAttribute = System.ComponentModel.DataAnnotations.Schema.ForeignKeyAttribute;

namespace fitness_terem.Model
{
    [Table ("clientTicket")]
    public class ClientTicket
    {
        [PrimaryKey, AutoIncrement]
        public int Client_ticket_id { get; set; }

        [ForeignKey(nameof(Client.Client_id))]
        public int Client_id { get; set; }

        [ForeignKey(nameof(TicketType.Ticket_id))]
        public int Ticket_id { get; set; }

        public DateTime Purchase_date { get; set; }

        public int Entry_count { get; set; }

        public int Purchase_price { get; set; }

        public bool Is_valid { get; set; }

        [ForeignKey(nameof(Gym.Gym_id))]
        public int Gym_id { get; set; }

    }
}
