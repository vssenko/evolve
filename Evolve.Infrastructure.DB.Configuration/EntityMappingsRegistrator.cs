using Evolve.Infrastructure.DB.Configuration.Mappings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Infrastructure.DB.Configuration
{
    public static class EntityMappingsRegistrator
    {
        public static void Register(ConfigurationRegistrar registrar)
        {
            registrar.Add(new UserMapping());
            registrar.Add(new UserDetailsMapping());
            registrar.Add(new UserCredentialsMapping());
            registrar.Add(new CommentMapping());
            registrar.Add(new PostboxMapping());
            registrar.Add(new PostMapping());
            registrar.Add(new PostBodyMapping());
        }
    }
}
