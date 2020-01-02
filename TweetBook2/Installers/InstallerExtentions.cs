using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetBook2.Installers
{
    public static class InstallerExtentions
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            // we select all installer we typeof(Startup).Assembly.ExportedTypes.Where(x =>
            //typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)

            // make instance of them : Select(Activator.CreateInstance)

            // then cast them as the interface :Cast<IInstaller>()
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
                typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            installers.ForEach(installer => installer.InstallerServices(services, configuration));
        }

    }
}
