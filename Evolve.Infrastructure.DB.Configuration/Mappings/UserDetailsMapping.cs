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
    class UserDetailsMapping : EntityTypeConfiguration<UserDetails>
    {
        public UserDetailsMapping()
        {
            ToTable("dbo.UserDetails");
            HasKey(x => x.UserId);
            Property(x => x.UserId).HasColumnName("UserId");
            Property(x => x.Firstname).HasColumnName("Firstname");
            Property(x => x.Lastname).HasColumnName("Lastname");
            Property(x => x.ImagePath).HasColumnName("ImagePath");
            Property(x => x.Summary).HasColumnName("Summary");
            Property(x => x.CreatedDate).HasColumnName("CreatedDate");
            Property(x => x.Rating).HasColumnName("Rating");
            HasRequired(x => x.User).WithRequiredDependent(x => x.UserDetails);
        }
    }
}
