using Evolve.Domain.UserAggr;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Infrastructure.DB.Configuration.Mappings
{
    public class UserMapping : EntityTypeConfiguration<User>
    {
        public UserMapping()
        {
            ToTable("dbo.User");
            HasKey(x => x.UserId);
            Property(x => x.UserId).HasColumnName("UserId");
            Property(x => x.Username).HasColumnName("Username");
            Property(x => x.Email).HasColumnName("Email");
            HasMany(x => x.Comments).WithRequired(y => y.User);
            HasMany(x => x.Posts).WithRequired(y => y.User);
            HasMany(x => x.Postboxes).WithRequired(y => y.User);
            HasMany(x => x.Subscribers).WithMany(x => x.Subscribtions).Map(x =>
                {
                    x.ToTable("UserSubscription");
                    x.MapLeftKey("SubscriberId");
                    x.MapRightKey("UserId");
                });
        }
    }
}
