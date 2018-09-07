using Evolve.Infrastructure.DB.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Infrastructure.DB.EF.Core
{
    public class DataContext : DbContext
    {
        public bool IsNoCommit { get; set; }

        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public DataContext()
            : base(ConnectionString)
        {
            Configuration.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Add(new TableNameConvention());
            EntityMappingsRegistrator.Register(modelBuilder.Configurations);
        }
    }
}
