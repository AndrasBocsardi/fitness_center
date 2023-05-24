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
using Entry = fitness_terem.Model.Entry;

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

        public async Task<List<ClientTicket>> GetAllTickets()
        {
            await Init();
            return await conn.Table<ClientTicket>().ToListAsync();
        }
        public async Task<List<TicketType>> GetAllTycketTypes()
        {
            await Init();
            return await conn.Table<TicketType>().ToListAsync();
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

        public async Task<int> AssignTicketToClient(int client_id, int ticket_id, int price)
        {
            await Init();
            return await conn.InsertAsync(new ClientTicket { Client_id = client_id, Ticket_id = ticket_id, Purchase_date = DateTime.Now, Entry_count = 0, Purchase_price = price, Is_valid = true, Gym_id = 1 });
        }

        public async Task<List<ClientTicket>> GetClientTickets(int clientId)
        {
            await Init();
            return await conn.Table<ClientTicket>()
                             .Where(ticket => ticket.Client_id == clientId)
                             .ToListAsync();
        }

        public async Task<Client> GetClient(string email)
        {
            await Init();
            var client = await conn.FindAsync<Client>(c => c.Email == email);
            return client;
        }
        public async Task<Gym> GetGymName(int gymId)
        {
            await Init();
            var gym = await conn.FindAsync<Gym>(g => g.Gym_id == gymId);
            return gym;
        }

        public async Task<Client> GetClientById(int id)
        {
            await Init();
            var client = await conn.FindAsync<Client>(c => c.Client_id == id);
            return client;
        }
        public async Task DeleteClient(int id)
        {
            await Init();
            var client=await conn.FindAsync<Client>(c => c.Client_id == id);
            await conn.DeleteAsync(client);
        }
        public async Task DeleteTicket(int id)
        {
            await Init();
            var ticket = await conn.FindAsync<ClientTicket>(t => t.Client_ticket_id==id);
            await conn.DeleteAsync(ticket);
        }
        public async Task UpdateEntryCount(int ticketId, int entryCount)
        {
            await Init();
            var ticket= await conn.FindAsync<ClientTicket>(c => c.Client_ticket_id == ticketId);
            ticket.Entry_count = entryCount;
            await conn.UpdateAsync(ticket);
           // return ticket;
        }

        public async Task<int> GetMaxNrOfEntries(int ticketId)
        {
            await Init();
            var nr = await conn.FindAsync<TicketType>(t =>t.Ticket_id==ticketId);
            return nr.Nr_of_entry_valid;
        }

        public async Task<int> GetNrOfDaysValid(int ticketId)
        {
            await Init();
            var nr = await conn.FindAsync<TicketType>(t => t.Ticket_id == ticketId);
            return nr.Nr_of_days_valid;
        }
        public async Task UpdateValidity(int ticketId)
        {
            await Init();
            var ticket = await conn.FindAsync<ClientTicket>(c => c.Client_ticket_id == ticketId);
            ticket.Is_valid = false; 
            await conn.UpdateAsync(ticket);

        }

        public async Task<int> InsertEntry(Entry entry)
        {
            await Init();
            return await conn.InsertAsync(entry);
        }

        public async Task<List<Gym>> GetAllGyms()
        {
            await Init();
            return await conn.Table<Gym>().ToListAsync();
        }

        public async Task<List<TicketType>> GetTicketsByGym(int gymId)
        {
            await Init();
            return await conn.Table<TicketType>()
                              .Where(g => g.Gym_id==gymId)
                              .ToListAsync();
        }
        public async Task<TicketType> AddNewTicket(string name, int price, int nr_of_days_valid, int nr_of_entry_valid, string startTime, string endTime)
        {
            await Init();
            TicketType newTicketType = new TicketType { Name = name, Price = price, Nr_of_days_valid = nr_of_days_valid, Nr_of_entry_valid = nr_of_entry_valid, Start_time_of_day = startTime, End_time_of_day = endTime, Gym_id = 1, Is_deleted = false };
            await conn.InsertAsync(newTicketType);

            return newTicketType;
        }
    }
}
