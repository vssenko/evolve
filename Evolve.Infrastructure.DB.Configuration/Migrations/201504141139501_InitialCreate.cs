namespace Evolve.Infrastructure.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable("dbo.User", c => new
            {
                UserId = c.Int(false, true),
                Username = c.String(false, 200),
                PasswordHash = c.String(false, 64)
            })
            .PrimaryKey(x => x.UserId)
            .Index(x => x.Username);
        }
        
        public override void Down()
        {
            DropTable("dbo.User");
        }
    }
}
