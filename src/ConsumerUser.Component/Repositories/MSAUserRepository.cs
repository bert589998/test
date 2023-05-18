// <copyright file="MSAUserRepository.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.CMD.Service.ConsumerUser.Component.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Bond;
    using Microsoft.CMD.Service.ConsumerUser.Common.Configuration.CosmosDB;
    using Microsoft.CMD.Service.ConsumerUser.Common.CosmosDB;
    using Microsoft.CMD.Service.ConsumerUser.Data.Entities.DataEntities;
    using Microsoft.Management.Services.CloudPC.Instrumentation;
    using Microsoft.Management.Services.CloudPC.Storage.CosmosDB.Interface;

    public class MSAUserRepository : IMSAUserRepository
    {
        private readonly IStorage cosmosDB;
        private readonly ILoggerX logger;
        private readonly string databaseId = CosmosDBMetadata.DatabaseName;
        private readonly string collectionId = CosmosDBCollectionNames.MSACollectionName;

        public MSAUserRepository(IStorage storage, ILoggerX logger)
        {
            this.cosmosDB = storage;
            this.logger = logger;
        }

        public async Task CreateMSAUserAsync(MSAUserDataEntity user)
        {
            try
            {
                await this.cosmosDB.PutEntity(logger, databaseId, collectionId, user);
            }
            catch (Exception ex)
            {
                this.logger.LogException("MSAUserRepository", "CreateMSAUserAsync", ex);
                throw;
            }
        }

        public async Task<MSAUserDataEntity> GetMSAUserAsync(string msaid, string? providerType = null)
        {
            try
            {
                return await this.cosmosDB.GetEntity<MSAUserDataEntity>(logger, databaseId, collectionId, MSAUserDataEntity.GetPartitionKey(msaid), MSAUserDataEntity.GetID(msaid));
            }
            catch (Exception ex)
            {
                this.logger.LogException("MSAUserRepository", "GetMSAUserAsync", ex);
                throw;
            }
        }

        public async Task UpdateMSAUserAsync(MSAUserDataEntity user)
        {
            try
            {
                await this.cosmosDB.WriteEntity(logger, databaseId, collectionId, user);
            }
            catch (Exception ex)
            {
                this.logger.LogException("MSAUserRepository", "UpdateMSAUserAsync", ex);
                throw;
            }
        }

        public async Task DeleteMSAUserAsync(string msaid, string? providerType = null)
        {
            try
            {
                await this.cosmosDB.DeleteEntity<MSAUserDataEntity>(logger, databaseId, collectionId, MSAUserDataEntity.GetPartitionKey(msaid), MSAUserDataEntity.GetID(msaid));
            }
            catch (Exception ex)
            {
                this.logger.LogException("UserRepository", "DeleteUserAsync", ex);
                throw;
            }
        }
    }
}
