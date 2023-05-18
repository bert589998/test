// <copyright file="UserRepository.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.CMD.Service.ConsumerUser.Component.Repositories
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.CMD.Service.ConsumerUser.Common.Configuration.CosmosDB;
    using Microsoft.CMD.Service.ConsumerUser.Common.CosmosDB;
    using Microsoft.CMD.Service.ConsumerUser.Data.Entities.DataEntities;
    using Microsoft.Management.Services.CloudPC.Instrumentation;
    using Microsoft.Management.Services.CloudPC.Storage.CosmosDB.Artifacts;
    using Microsoft.Management.Services.CloudPC.Storage.CosmosDB.Interface;

    /// <summary>
    /// A sample repository.
    /// An interface to access a CosmosDB collection.
    /// ASP.Net provides an IStorage singleton to access CosmosDB in a lower level.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class UserRepository : IUserRepository
    {
        private readonly IStorage cosmosDB;
        private readonly ILoggerX logger;
        private readonly string databaseId = CosmosDBMetadata.DatabaseName;
        private readonly string collectionId = CosmosDBCollectionNames.UserCollectionName;

        public UserRepository(IStorage storage, ILoggerX logger)
        {
            this.cosmosDB = storage;
            this.logger = logger;
        }

        public async Task<UserDataEntity> GetUserAsync(Guid cuid)
        {
            try
            {
                return await this.cosmosDB.GetEntity<UserDataEntity>(logger, databaseId, collectionId, UserDataEntity.GetPartitionKey(cuid), UserDataEntity.GetID(cuid));
            }
            catch (Exception ex)
            {
                this.logger.LogException("UserRepository", "GetUserAsync", ex);
                throw;
            }
        }

        public async Task CreateUserAsync(UserDataEntity user)
        {
            try
            {
                await this.cosmosDB.PutEntity(logger, databaseId, collectionId, user);
            }
            catch (Exception ex)
            {
                this.logger.LogException("UserRepository", "CreateUserAsync", ex);
                throw;
            }
        }

        public async Task UpdateUserAsync(UserDataEntity user)
        {
            try
            {
                await this.cosmosDB.WriteEntity(logger, databaseId, collectionId, user);
            }
            catch (Exception ex)
            {
                this.logger.LogException("UserRepository", "UpdateUserAsync", ex);
                throw;
            }
        }

        public async Task DeleteUserAsync(Guid cuid)
        {
            try
            {
                await this.cosmosDB.DeleteEntity<UserDataEntity>(logger, databaseId, collectionId, UserDataEntity.GetPartitionKey(cuid), UserDataEntity.GetID(cuid));
            }
            catch (Exception ex)
            {
                this.logger.LogException("UserRepository", "DeleteUserAsync", ex);
                throw;
            }
        }
    }
}
