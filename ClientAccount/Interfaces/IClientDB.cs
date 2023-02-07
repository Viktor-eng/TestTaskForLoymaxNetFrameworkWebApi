using ClientAccount.Models;
using System.Data.Entity;

namespace ClientAccount.DataBase
{
    public interface IClientDB
    {
        DbSet<Account> Accounts { get; set; }

        DbSet<Client> Clients { get; set; }
    }
}
