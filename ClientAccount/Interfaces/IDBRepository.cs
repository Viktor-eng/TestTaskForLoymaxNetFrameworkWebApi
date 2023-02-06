using ClientAccount.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientAccount.DataBase
{
    public interface IDBRepository
    {
        Task AddClient(Client client);

        Task<IEnumerable<Account>> GetAccounts();

        Task<Client> GetClients(int id);

        Task UpdateClient(int id, Client newClient);
    }
}
