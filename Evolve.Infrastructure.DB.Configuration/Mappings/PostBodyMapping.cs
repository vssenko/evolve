using Evolve.Domain.PostAggr;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Infrastructure.DB.Configuration.Mappings
{
    public class PostBodyMapping : EntityTypeConfiguration<PostBody>
    {
        public PostBodyMapping()
        {
            ToTable("dbo.PostBody");
            HasKey(x => x.PostId);
            Property(x => x.PostId).HasColumnName("PostId");
            HasRequired(x => x.Post).WithRequiredDependent(x => x.PostBody);
        }
    }
}
