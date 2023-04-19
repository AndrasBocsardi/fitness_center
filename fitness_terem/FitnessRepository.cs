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

        public async Task AddNewClient(string name, string email, string password, string address, string phone_nr)
        {
            await Init();
            await conn.InsertAsync(new Client {Name = name, Email = email, Password = password, Address = address, Phone_nr = phone_nr, Inserted_date = DateTime.Now, Description = "" });
            
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
       
    }
}
