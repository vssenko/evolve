using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Infrastructure.DB.Configuration
{
    public class TableNameConvention : Convention
    {
        public TableNameConvention()
        {
            base.Types().Configure(x => x.ToTable(x.ClrType.Name));
        }
    }
}
