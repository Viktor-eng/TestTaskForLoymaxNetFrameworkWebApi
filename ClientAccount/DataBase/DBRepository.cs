using ClientAccount.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ClientAccount.DataBase
{
    public class DBRepository : IDBRepository
    {
        readonly IClientDB _clientDB = new ClientDB();


        public DBRepository(IClientDB clientDB)
        {
            _clientDB = clientDB;
        }


        public DBRepository() { }


        public async Task<IEnumerable<Account>> GetAccounts()
        {
            using (var clientDB = new ClientDB())
            {
                return await clientDB.Accounts.ToListAsync();
            }
        }

        public async Task<Client> GetClients(int id)
        {
            using (var clientDB = new ClientDB())
            {
                return await clientDB
                    .Clients.Include(x => x.Account)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task AddClient(Client client)
        {
            using (var clientDB = new ClientDB())
            {
                clientDB.Clients.Add(client);
                await clientDB.SaveChangesAsync();
            }
        }

        public async Task UpdateClient(int id, Client newClient)
        {
            using (var clientDB = new ClientDB())
            {
                var oldClient = await clientDB
                     .Clients.Include(x => x.Account)
                     .FirstOrDefaultAsync(x => x.Id == id);

                oldClient.Name = newClient.Name;
                oldClient.Patronymic = newClient.Patronymic;
                oldClient.LastName = newClient.LastName;
                oldClient.BirthDate = newClient.BirthDate;
                oldClient.Account = newClient.Account;
                oldClient.Account.Balance= newClient.Account.Balance;
                oldClient.Account.ClientId = newClient.Account.ClientId;
 
                await clientDB.SaveChangesAsync();
            }
        }
    }
}
