using Evolve.Domain.PostAggr;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Infrastructure.DB.Configuration.Mappings
{
    class PostMapping : EntityTypeConfiguration<Post>
    {
        public PostMapping()
        {
            ToTable("dbo.Post");
            HasKey(x => x.PostId);
            Property(x => x.PostId).HasColumnName("PostId");
            Property(x => x.Title).HasColumnName("Title");
            Property(x => x.CreatedDate).HasColumnName("CreatedDate");
            Property(x => x.PostboxId).HasColumnName("PostboxId");
            Property(x => x.UserId).HasColumnName("UserId");
            HasMany(x => x.Comments).WithRequired(y => y.Post).HasForeignKey(z => z.PostId);
            HasRequired(x => x.User).WithMany(z => z.Posts);
            HasOptional(x => x.Postbox).WithMany(y => y.Posts);
        }
    }
}
