// <copyright file="IHealthCheckRepository.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.CMD.Service.ConsumerUser.Common.CosmosDB.Repositories
{
    using Microsoft.CMD.Service.ConsumerUser.Common.Configuration.CosmosDB;
    using Microsoft.CMD.Service.ConsumerUser.Common.CosmosDB.Entities;
    using Microsoft.Management.Services.CloudPC.Instrumentation;
    using Microsoft.Management.Services.CloudPC.Storage.CosmosDB.Artifacts;
    using Microsoft.Management.Services.CloudPC.Storage.CosmosDB.Interface;

    /// <summary>
    /// A sample repository.
    /// An interface to access a CosmosDB collection.
    /// ASP.Net provides an IStorage singleton to access CosmosDB in a lower level.
    /// </summary>
    public interface IHealthCheckRepository
    {
        Task<HealthCheckEntity> GetOrCreateEntity(string name);

        Task PutEntity(HealthCheckEntity entity);
    }

    public class HealthCheckRepository : IHealthCheckRepository
    {
        private readonly IStorage storage;
        private readonly ILoggerX logger;

        // Auto-wired parameters
        public HealthCheckRepository(IStorage storage, ILoggerX logger)
        {
            this.storage = storage;
            this.logger = logger;
        }

        public async Task<HealthCheckEntity> GetOrCreateEntity(string name)
        {
            var artifacts = GetQueryArtifactsByName(name);

            var results = await storage.GetEntities<HealthCheckEntity>(
                logger: logger,
                dbId: CosmosDBMetadata.DatabaseName,
                collectionId: CosmosDBCollectionNames.HealthCheckCollection,
                id: "HealthCheck Logs",
                artifacts: artifacts);

            return results.Values.FirstOrDefault(
                new HealthCheckEntity()
                {
                    ItemId = Guid.NewGuid(),
                    Name = name,
                    Count = 0
                });
        }

        public async Task PutEntity(HealthCheckEntity entity)
        {
            await storage.WriteEntityWithResponse<HealthCheckEntity>(
                logger: logger,
                dbId: CosmosDBMetadata.DatabaseName,
                collectionId: CosmosDBCollectionNames.HealthCheckCollection,
                obj: entity);
        }

        private QueryArtifacts GetQueryArtifactsByName(string name)
        {
            var artifacts = new QueryArtifacts();
            artifacts.Clauses.Add(new ConditionalClause(ConditionalOperator.Equal, "name", name));
            return artifacts;
        }
    }
}
