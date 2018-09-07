using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Infrastructure.DB.Configuration
{
    class SetupDataContext: DbContext
    {
        public bool IsNoCommit { get; set; }

        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public SetupDataContext()
            : base(ConnectionString)
        {
            Configuration.AutoDetectChangesEnabled = false;
        }
    }
}
