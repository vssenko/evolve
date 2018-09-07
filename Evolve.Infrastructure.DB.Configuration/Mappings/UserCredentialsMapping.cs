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
    class UserCredentialsMapping : EntityTypeConfiguration<UserCredentials>
    {
        public UserCredentialsMapping()
        {
            ToTable("dbo.UserCredentials");
            HasKey(x => x.UserId);
            Property(x => x.PasswordHash).HasColumnName("PasswordHash");
            Property(x => x.UserId).HasColumnName("UserId");
            HasRequired(x => x.User).WithRequiredDependent(x => x.UserCredentials);
        }
    }
}
