namespace Evolve.Infrastructure.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MissingReferences : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comment", "PostId", c => c.Int(false));
            AddForeignKey("dbo.Comment", "PostId", "dbo.Post", "PostId");
            AddColumn("dbo.Postbox", "UserId", c => c.Int(false));
            AddForeignKey("dbo.Postbox", "UserId", "dbo.User", "UserId");
            AddColumn("dbo.Post", "UserId", c => c.Int(false));
            AddForeignKey("dbo.Post", "UserId", "dbo.User", "UserId");
        }
        
        public override void Down()
        {
        }
    }
}
