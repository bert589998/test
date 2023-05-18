// <copyright file="MSAUserDataEntity.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.CMD.Service.ConsumerUser.Data.Entities.DataEntities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.CMD.Service.ConsumerUser.Data.Enums;
    using Microsoft.Management.Services.CloudPC.Storage.CosmosDB.Entities;
    using Newtonsoft.Json;

    public class MSAUserDataEntity : EntityBase
    {
        public MSAUserDataEntity()
        {
        }

        public MSAUserDataEntity(string msaid, string providerType)
        {
            CUID = UserDataEntity.GetCUID(msaid, providerType);
            MSAID = msaid;
            ProviderType = providerType;

            UniqueId = GetID(msaid, providerType);
            PartitionKey = GetPartitionKey(msaid);
        }

        /// <summary>
        /// Represents consumer Id.
        /// </summary>
        [JsonProperty("cuid")]
        public Guid CUID { get; set; }

        /// <summary>
        /// Represents microsoft account Id.
        /// </summary>
        [JsonProperty("msaid")]
        public string MSAID { get; set; }

        /// <summary>
        /// Represents provider type.
        /// </summary>
        [JsonProperty("providerType")]
        public string ProviderType { get; set; }

        /// <summary>
        /// Represents data insertion time.
        /// </summary>
        [JsonProperty("createDate")]
        public DateTime CreateDate { get; set; }

        public static string GetID(string msaid, string? providerType = null)
        {
            return $"Id_{msaid}_{providerType}";
        }

        public static string GetPartitionKey(string msaid)
        {
            return $"MSA_{msaid}";
        }
    }
}
