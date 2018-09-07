using Evolve.Domain.PostAggr;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Infrastructure.DB.Configuration.Mappings
{
    class PostboxMapping : EntityTypeConfiguration<Postbox>
    {
        public PostboxMapping()
        {
            ToTable("dbo.Postbox");
            HasKey(x => x.PostboxId);
            Property(x => x.PostboxId).HasColumnName("PostboxId");
            Property(x => x.Name).HasColumnName("Name");
            Property(x => x.UserId).HasColumnName("UserId");
            Property(x => x.CreatedDate).HasColumnName("CreatedDate");
            HasMany(x => x.Posts).WithOptional(y => y.Postbox);
            HasRequired(x => x.User).WithMany(y => y.Postboxes);
        }
    }
}
