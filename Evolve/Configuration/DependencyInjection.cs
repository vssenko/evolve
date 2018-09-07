using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Evolve.Application.Services;
using Evolve.Infrastructure.DB.EF.Core;
using Evolve.Infrastructure.DB.EF.Repository;
using Evolve.Modules;
using Evolve.Infrastructure;
using Evolve.Models;
using Nancy.Authentication.Forms;


namespace Evolve.Configuration
{
    static class DependencyInjection
    {
        public static void Setup(ContainerBuilder builder)
        {
            //Database infrastructure
            builder.RegisterType<DataContext>()
                .InstancePerRequest();
            builder.RegisterGeneric(typeof(UnitOfWork<>))
                .As(typeof(IUnitOfWork<>))
                .InstancePerRequest();
            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .InstancePerRequest();

            //Infrastructure
            builder.RegisterType<HashProvider>()
                .As<IHashProvider>()
                .InstancePerRequest();
            builder.RegisterType<FileManager>()
                .As<IFileManager>()
                .InstancePerRequest();


            //Business logic
            builder.RegisterType<UserService>()
                .As<IUserService>()
                .InstancePerRequest();
            builder.RegisterType<PostService>()
                .As<IPostService>()
                .InstancePerRequest();
            builder.RegisterType<SearchService>()
                .As<ISearchService>()
                .InstancePerRequest();


            builder.RegisterType<UserMapper>()
                .As<IUserMapper>()
                .InstancePerRequest();
        }
    }
}
