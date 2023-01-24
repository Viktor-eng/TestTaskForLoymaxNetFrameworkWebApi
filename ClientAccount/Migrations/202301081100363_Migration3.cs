namespace ClientAccount.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Accounts", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "Id", c => c.Int(nullable: false));
        }
    }
}
