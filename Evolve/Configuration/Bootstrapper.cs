using Autofac;
using Nancy.Bootstrappers.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using System.Threading.Tasks;
using Nancy.Responses;
using Nancy.Authentication.Forms;
using System.IO;

namespace Evolve.Configuration
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        protected override void ApplicationStartup(ILifetimeScope container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            var imgDirectoryPath = Directory.GetCurrentDirectory() + "/img/";
            if (!Directory.Exists(imgDirectoryPath))
            {
                Directory.CreateDirectory(imgDirectoryPath);
            }
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);
            var builder = new ContainerBuilder();
            DependencyInjection.Setup(builder);
            builder.Update(existingContainer.ComponentRegistry);
        }

        protected override void RequestStartup(ILifetimeScope container, Nancy.Bootstrapper.IPipelines pipelines, Nancy.NancyContext context)
        {
            var formsAuthConfiguration =
                new FormsAuthenticationConfiguration()
                {
                    RedirectUrl = "~/login",
                    UserMapper = container.Resolve<IUserMapper>(),
                };
            FormsAuthentication.Enable(pipelines, formsAuthConfiguration);

        }
    }
}
