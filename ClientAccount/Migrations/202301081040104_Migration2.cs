namespace ClientAccount.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Migration2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Accounts", "Id", "dbo.Clients");
            DropIndex("dbo.Accounts", new[] { "Id" });
            DropPrimaryKey("dbo.Accounts");
            AddColumn("dbo.Accounts", "ClientId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Accounts", "ClientId");
            CreateIndex("dbo.Accounts", "ClientId");
            AddForeignKey("dbo.Accounts", "ClientId", "dbo.Clients", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "ClientId", "dbo.Clients");
            DropIndex("dbo.Accounts", new[] { "ClientId" });
            DropPrimaryKey("dbo.Accounts");
            DropColumn("dbo.Accounts", "ClientId");
            AddPrimaryKey("dbo.Accounts", "Id");
            CreateIndex("dbo.Accounts", "Id");
            AddForeignKey("dbo.Accounts", "Id", "dbo.Clients", "Id");
        }
    }
}
