namespace Evolve.Infrastructure.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PKUserEntitiesFix : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserDetails");
            DropPrimaryKey("dbo.UserCredentials");
            DropColumn("dbo.UserDetails", "UserDetailsId");
            DropColumn("dbo.UserCredentials", "UserCredentialsId");
            AddPrimaryKey("dbo.UserDetails", "UserId");
            AddPrimaryKey("dbo.UserCredentials", "UserId");
        }
        
        public override void Down()
        {
        }
    }
}
