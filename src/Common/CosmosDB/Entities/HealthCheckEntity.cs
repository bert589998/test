// <copyright file="HealthCheckEntity.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.CMD.Service.ConsumerUser.Common.CosmosDB.Entities
{
    using Microsoft.Management.Services.CloudPC.Storage.CosmosDB.Entities;
    using Newtonsoft.Json;

    /// <summary>
    /// Sample entity (aka model) definition.
    /// HealthCheckEntity reprensents a (name => counter) relation.
    /// Note that a Guid field is mandatory as the id of document. The name does not matter (?).
    /// </summary>
    public class HealthCheckEntity : EntityBase
    {
        [JsonProperty("id")]
        public Guid ItemId { get; set; } // ID Required

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
