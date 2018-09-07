namespace Evolve.Infrastructure.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserSubscription : DbMigration
    {
        public override void Up()
        {
            CreateTable("UserSubscription", x => new
            {
                SubscriberId = x.Int(false, false),
                UserId = x.Int(false, false)
            });
            CreateIndex("UserSubscription", "SubscriberId");
            CreateIndex("UserSubscription", "UserId");
            AddForeignKey("UserSubscription", "SubscriberId","User","UserId");
            AddForeignKey("UserSubscription", "UserId","User","UserId");
        }
        
        public override void Down()
        {
        }
    }
}
