using System.Data.Entity;
using ClientAccount.Models;

namespace ClientAccount.DataBase
{
    public class ClientDB : DbContext, IClientDB
    {
        public ClientDB()
            : base("name=ClientDB")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ClientDB>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ClientDB>());
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasKey(x => x.ClientId);

            modelBuilder.Entity<Client>()
                .HasRequired(x => x.Account)
                .WithRequiredPrincipal(x => x.Client);

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<Account> Accounts { get; set; }
    }
}
