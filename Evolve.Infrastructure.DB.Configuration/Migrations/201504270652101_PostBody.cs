namespace Evolve.Infrastructure.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostBody : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Post", "Body");
            CreateTable("dbo.PostBody", x => new
                {
                    PostId = x.Int(false),
                    Body = x.String(false)
                }).PrimaryKey(x => x.PostId);
            AddForeignKey("dbo.PostBody", "PostId", "dbo.Post", "PostId", true);
        }
        
        public override void Down()
        {
        }
    }
}
