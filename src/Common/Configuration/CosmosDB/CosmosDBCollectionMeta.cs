// <copyright file="CosmosDBCollectionMeta.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.CMD.Service.ConsumerUser.Common.Configuration.CosmosDB
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Management.Services.CloudPC.Storage.CosmosDB.Artifacts;

    /// <summary>
    /// CosmosDB collection information.
    /// Defined in CosmosDBMetadata.
    /// Consumed by CosmosDBInitializer
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CosmosDBCollectionMeta
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CosmosDBCollectionModel"/> class.
        /// </summary>
        /// <param name="collectionId">Collection name.</param>
        /// <param name="partitionKey">Partition key column name.</param>
        /// <param name="ttl">TTL.</param>
        public CosmosDBCollectionMeta(string collectionId, string partitionKey, int ttl)
        {
            this.CollectionId = collectionId;
            this.PartitionKey = partitionKey;
            this.Ttl = ttl;
        }

        /// <summary>
        /// Gets of collection ID.
        /// </summary>
        public string CollectionId { get; internal set; }

        /// <summary>
        /// Gets of partition key.
        /// </summary>
        public string PartitionKey { get; internal set; }

        /// <summary>
        /// Gets of time to live.
        /// </summary>
        public int Ttl { get; internal set; } = -1;

        /// <summary>
        /// Gets properties need index.
        /// </summary>
        public List<string> IndexProperties { get; internal set; }

        /// <summary>
        /// Gets composite indexes.
        /// </summary>
        public List<IEnumerable<CompositeIndex>> CompositeIndexProperties { get; internal set; }
    }
}
