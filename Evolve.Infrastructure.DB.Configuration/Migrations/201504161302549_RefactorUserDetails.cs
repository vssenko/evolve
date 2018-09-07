namespace Evolve.Infrastructure.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorUserDetails : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.UserCredentials", "IX_Email");
            DropColumn("dbo.UserCredentials", "Email");
            AddColumn("dbo.User", "Email", x => x.String(false, 200));
            CreateIndex("dbo.User","Email");
            DropColumn("dbo.User", "Firstname");
            DropColumn("dbo.User", "Lastname");
            AddColumn("dbo.UserDetails", "Firstname", c => c.String(false, 50));
            AddColumn("dbo.UserDetails", "Lastname", c => c.String(false, 50));
            CreateIndex("dbo.UserDetails", "Firstname");
            CreateIndex("dbo.UserDetails", "Lastname");
        }
        
        public override void Down()
        {
        }
    }
}
