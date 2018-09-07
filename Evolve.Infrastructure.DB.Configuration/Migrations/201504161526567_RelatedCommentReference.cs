namespace Evolve.Infrastructure.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelatedCommentReference : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comment", "RelatedCommentId", c => c.Int(true));
            AddForeignKey("dbo.Comment", "RelatedCommentId", "dbo.Comment", "CommentId");
        }
        
        public override void Down()
        {
        }
    }
}
