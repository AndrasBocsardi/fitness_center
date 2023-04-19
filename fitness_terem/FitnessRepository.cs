using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using fitness_terem.Model;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Net;
using System.Xml.Linq;

namespace fitness_terem
{
    public class FitnessRepository
    {
        string _dbPath;

        private SQLiteAsyncConnection conn;
        private async Task Init()
        {
            
            
            if (conn is not null)
                return;

            conn = new SQLiteAsyncConnection(_dbPath);
            
            await conn.CreateTableAsync<Client>();
            await conn.CreateTableAsync<Gym>();
            await conn.CreateTableAsync<TicketType>();
            await conn.CreateTableAsync<ClientTicket>();
            await conn.CreateTableAsync<Model.Entry>();
        }

        public FitnessRepository(string dbPath)
        {
            _dbPath= dbPath;
        }

        public async Task<int> AddNewClient(string name, string email, string password, string address, string phone_nr)
        {
            await Init();
            return await conn.InsertAsync(new Client {Name = name, Email = email, Password = password, Address = address, Phone_nr = phone_nr, Inserted_date = DateTime.Now, Description = "" });
            
        }

        public async Task<List<Client>> GetAllClients()
        {          
                await Init();
                return await conn.Table<Client>().ToListAsync();
        }
        


        public async Task<Boolean> CheckLogin(string email, string password)
        {
            await Init();
            var client = await conn.FindAsync<Client>(c =>  c.Email == email && c.Password == password );
            return client != null;
        }

       public async Task<int> GetUserIdByEmail(string email)
        {
            await Init();
            var client = await conn.FindAsync<Client>(c => c.Email == email);

            return client.Client_id;
        }

        public async Task<Boolean> ChechIfAdmin(string email)
        {
            await Init();
            var client = await conn.FindAsync<Client>(c => c.Email == email);

            return client.Description == "admin";
        }

        public async Task<TicketType> AddNewTicket(string name, int price, int nr_of_days_valid, int nr_of_entry_valid, string startTime, string endTime)
        {
            await Init();
            TicketType newTicketType = new TicketType { Name = name, Price = price, Nr_of_days_valid = nr_of_days_valid, Nr_of_entry_valid = nr_of_entry_valid, Start_time_of_day = startTime, End_time_of_day = endTime, Gym_id = 1, Is_deleted = false };
            await conn.InsertAsync(newTicketType);

            return newTicketType;
        }

        public async Task<int> AssignTicketToClient(int client_id, int ticket_id, int price)
        {
            await Init();
            return await conn.InsertAsync(new ClientTicket { Client_id = client_id, Ticket_id = ticket_id, Purchase_date = DateTime.Now, Entry_count = 0, Purchase_price = price, Is_valid = true, Gym_id = 1 });
        }

       
    }
}
