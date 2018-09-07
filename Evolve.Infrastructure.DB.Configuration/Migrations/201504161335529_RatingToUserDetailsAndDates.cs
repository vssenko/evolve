namespace Evolve.Infrastructure.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingToUserDetailsAndPostDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDetails", "Rating", c => c.Int(false));
            DropColumn("dbo.User", "Rating");
            AddColumn("dbo.UserDetails", "CreatedDate", c => c.DateTime(false));
            AddColumn("dbo.Post", "CreatedDate", c => c.DateTime(false));
            AddColumn("dbo.Postbox", "CreatedDate", c => c.DateTime(false));
            AddColumn("dbo.Comment", "CreatedDate", c => c.DateTime(false));
        }
        
        public override void Down()
        {
        }
    }
}
