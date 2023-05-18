// <copyright file="CosmosDBMetadata.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.CMD.Service.ConsumerUser.Common.Configuration.CosmosDB
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Diagnostics.CodeAnalysis;
    using Names = Microsoft.CMD.Service.ConsumerUser.Common.CosmosDB.CosmosDBCollectionNames;

    /// <summary>
    /// The class encapsulate metadata for database.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class CosmosDBMetadata
    {
        // TODO: Update database name
        public static readonly string DatabaseName = "ConsumerUserService";

        public static readonly CosmosDBCollectionMeta HealthCheckCollection = new CosmosDBCollectionMeta(
            collectionId: Names.HealthCheckCollection,
            partitionKey: "partitionKey",
            ttl: NeverExpire);

        public static readonly CosmosDBCollectionMeta UserRepositoryCollection = new CosmosDBCollectionMeta(
            collectionId: Names.UserCollectionName,
            partitionKey: "partitionKey",
            ttl: NeverExpire);

        public static readonly CosmosDBCollectionMeta MSARepositoryCollection = new CosmosDBCollectionMeta(
            collectionId: Names.MSACollectionName,
            partitionKey: "partitionKey",
            ttl: NeverExpire);

        public static readonly ImmutableArray<CosmosDBCollectionMeta> Collections = ImmutableArray.Create(
            HealthCheckCollection,
            UserRepositoryCollection,
            MSARepositoryCollection /* TODO: Attach collections here */);

        private const int NeverExpire = -1;
    }
}
