using ClientAccount.Interfaces;
using ClientAccount.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;

namespace ClientAccount.DataBase
{
    public class DBRepository : IDBRepository
    {
        //readonly ClientDB _clientDB = new ClientDB();

        /*
        public DBRepository(ClientDB clientDB)
        {
            _clientDB = clientDB;
        }
        */

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

        public async Task AddOrUpdateClient(Client client)
        {
            using (var clientDB = new ClientDB())
            {
                clientDB.Clients.AddOrUpdate(client);
                await clientDB.SaveChangesAsync();
            }
        }
    }
}