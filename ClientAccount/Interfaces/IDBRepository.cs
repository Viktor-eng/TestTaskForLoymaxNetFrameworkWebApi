using ClientAccount.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientAccount.Interfaces
{
    public interface IDBRepository
    {
        Task<Client> GetClients(int id);

        Task<IEnumerable<Account>> GetAccounts();

        Task AddOrUpdateClient(Client client);
    }
}
