// <copyright file="CosmosDBInitializer.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.CMD.Service.ConsumerUser.Common.Utils.CosmosDB
{
    using Microsoft.CMD.Service.ConsumerUser.Common.Configuration;
    using Microsoft.CMD.Service.ConsumerUser.Common.Configuration.CosmosDB;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Management.Services.CloudPC.Instrumentation;
    using Microsoft.Management.Services.CloudPC.Storage.CosmosDB;
    using Microsoft.Management.Services.CloudPC.Storage.CosmosDB.Extension;

    /// <summary>
    /// CosmosDB connection helper.
    /// What it does:
    /// * Parse CosmosDB config from appsettings.json
    /// * Create CosmosDB collections according to CosmosDBMetadata.Collections
    /// </summary>
    public static class CosmosDBInitializer
    {
        public static void SetupDatabase(IServiceCollection services, ILoggerX logger, IConfiguration configuration)
        {
            logger?.LogInformation("Startup", nameof(SetupDatabase), new KeyValuePair<string, string>("Message", "Start set up database"));
            var cosmosDBConfig = BuildCosmosDBConfig(configuration);

            #region Initialize Cosmos DB Collections
            services.AddCosmosDBStorage(configuration, cosmosDBConfig, manager =>
            {
                manager.CreateDb(cosmosDBConfig.DatabaseName, cosmosDBConfig.ThroughPut);

                foreach (var dbCollection in CosmosDBMetadata.Collections)
                {
                    manager.CreateCollection(
                        dbId: cosmosDBConfig.DatabaseName,
                        collectionId: dbCollection.CollectionId,
                        partitionKey: dbCollection.PartitionKey,
                        ttl: dbCollection.Ttl);

                    #region Create Index
                    var indexExists = (dbCollection.IndexProperties?.Any() ?? false) || (dbCollection.CompositeIndexProperties?.Any() ?? false);
                    if (indexExists)
                    {
                        manager.UpdateIndexingPolicy(
                            dbId: cosmosDBConfig.DatabaseName,
                            collectionId: dbCollection.CollectionId,
                            indexProperties: dbCollection.IndexProperties,
                            compositeIndexProperties: dbCollection.CompositeIndexProperties);
                    }
                    #endregion
                }
            });
            #endregion
        }

        private static string GetEndpointUrl(IConfiguration configuration)
        {
            CosmosDBSettings cosmosdbSettings = configuration.GetSection("AzureResource:CosmosDb").Get<CosmosDBSettings>();
            Runtime runtime = configuration.GetSection("Runtime").Get<Runtime>();
            bool useKeyVault = runtime != null && runtime.UseKeyVault;

            if (useKeyVault)
            {
                return $"https://{cosmosdbSettings.AccountName}.{cosmosdbSettings.DocumentDbEndpoint}";
            }
            else
            {
                return $"https://{cosmosdbSettings.DocumentDbEndpoint}";
            }
        }

        private static CosmosDBConfig BuildCosmosDBConfig(IConfiguration configuration)
        {
            string cosmosDBEndpoint = GetEndpointUrl(configuration);
            int cosmosDBThroughput = int.Parse(configuration["customSettings:CosmosDBThroughput"]);

            return new CosmosDBConfig()
            {
                DatabaseName = CosmosDBMetadata.DatabaseName,
                EndpointUrl = cosmosDBEndpoint,
                ThroughPut = cosmosDBThroughput
            };
        }
    }
}
