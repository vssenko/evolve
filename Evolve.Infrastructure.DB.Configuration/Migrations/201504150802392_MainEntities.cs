namespace Evolve.Infrastructure.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MainEntities : DbMigration
    {
        public override void Up()
        {
            //User 
            DropIndex("dbo.User", "IX_Username");
            DropColumn("dbo.User", "Username");            
            DropColumn("dbo.User", "PasswordHash");
            CreateTable("dbo.UserCredentials", c => new
            {
                UserCredentialsId = c.Int(false,true),
                UserId = c.Int(false, false),
                Email = c.String(false, 200),
                PasswordHash = c.String(false, 64)
            })
            .PrimaryKey(x => x.UserCredentialsId)
            .Index(x => x.Email);
            AddForeignKey("dbo.UserCredentials", "UserId", "dbo.User", "UserId");
            AddColumn("dbo.User", "Firstname", c => c.String(false, 50));
            AddColumn("dbo.User", "Lastname", c => c.String(false, 50));
            AddColumn("dbo.User", "Username", c => c.String(false, 50));
            AddColumn("dbo.User", "Rating", c => c.Int(true,false));
            CreateTable("dbo.UserDetails", c => new
            {
                UserDetailsId = c.Int(false, true),
                UserId = c.Int(false, false),
                ImagePath = c.String(true),
                Summary = c.String(true)
            })
            .PrimaryKey(x => x.UserDetailsId);
            AddForeignKey("dbo.UserDetails", "UserId", "dbo.User", "UserId");
            CreateTable("dbo.Postbox", c => new 
            {
                PostboxId = c.Int(false,true),
                Name = c.String(false,500)
            })
            .PrimaryKey(x => x.PostboxId)
            .Index(x => x.Name);
            CreateTable("dbo.Post", c => new
            {
                PostId = c.Int(false, true),
                PostboxId = c.Int(true),
                Title = c.String(false, 1000),
                Body = c.String(false)
            })
            .PrimaryKey(x => x.PostId)
            .Index(x => x.Title);
            AddForeignKey("dbo.Post", "PostboxId", "dbo.Postbox", "PostboxId");
            CreateTable("dbo.Comment", c => new
            {
                CommentId = c.Int(false, true),
                UserId = c.Int(false),
                Body = c.String(false, 20000)
            })
            .PrimaryKey(x => x.CommentId);
            AddForeignKey("dbo.Comment", "UserId", "dbo.User", "UserId");

        }
        
        public override void Down()
        {
        }
    }
}
