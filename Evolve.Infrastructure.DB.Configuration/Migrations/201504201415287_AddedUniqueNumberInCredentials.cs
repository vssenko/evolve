namespace Evolve.Infrastructure.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUniqueNumberInCredentials : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserCredentials", "UniqueNumber", c => c.Guid(false, true));
            CreateIndex("dbo.UserCredentials", "UniqueNumber", true);
        }
        
        public override void Down()
        {
        }
    }
}
