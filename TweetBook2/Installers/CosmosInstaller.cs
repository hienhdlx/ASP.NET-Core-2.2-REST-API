using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmonaut;
using Cosmonaut.Extensions.Microsoft.DependencyInjection;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TweetBook2.Domain;

namespace TweetBook2.Installers
{
    public class CosmosInstaller : IInstaller
    {
        public void InstallerServices(IServiceCollection services, IConfiguration configuration)
        {
            //create configuration 
            //var cosmosStoreSetting = new CosmosStoreSettings(
            //    configuration["CosmosSettings: AccountUri"],
            //    configuration["CosmosSettings: AccountUri"],
            //    configuration["CosmosSettings: AccountKey"], 
            //    new ConnectionPolicy { ConnectionMode = ConnectionMode.Direct, ConnectionProtocol = Protocol.Tcp});

            //services.AddCosmosStore<Post>(cosmosStoreSetting);
        }
    }
}
