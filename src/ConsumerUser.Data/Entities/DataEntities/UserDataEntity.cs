// <copyright file="UserDataEntity.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.CMD.Service.ConsumerUser.Data.Entities.DataEntities
{
    using System.Diagnostics;
    using System.Security.Cryptography;
    using System.Text;
    using Microsoft.Azure.Management.AppService.Fluent.Models;
    using Microsoft.CMD.Service.ConsumerUser.Data.Enums;
    using Microsoft.Management.Services.CloudPC.Storage.CosmosDB.Entities;
    using Newtonsoft.Json;

    /// <summary>
    /// Sample entity (aka model) definition.
    /// Guideline: https://microsoft.visualstudio.com/DefaultCollection/CMD/_git/CMD-Redist-Common?path=%2Fsrc%2FLibraries%2FCosmosDBWrapper%2Fdocs%2Freadme.md&anchor=getentity&_a=preview
    /// </summary>
    public class UserDataEntity : EntityBase
    {
        public UserDataEntity()
        {
        }

        public UserDataEntity(string msaid, string providerType)
        {
            CUID = GetCUID(msaid, providerType);
            MSAID = msaid;
            Status = UserStatus.Active;
            GroupIds = new List<string>();
            CreateDate = DateTime.UtcNow;
            LastModifiedDate = DateTime.UtcNow;

            UniqueId = GetID(CUID);
            PartitionKey = GetPartitionKey(CUID);
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
        /// Represents user principal name.
        /// </summary>
        [JsonProperty("upn")]
        public string UPN { get; set; }

        /// <summary>
        /// Represents user status.
        /// </summary>
        [JsonProperty("status")]
        public UserStatus Status { get; set; }

        /// <summary>
        /// Represents the group ids that this user belongs to.
        /// </summary>
        [JsonProperty("groupIds")]
        public IList<string> GroupIds { get; set; }

        /// <summary>
        /// Represents data insertion time.
        /// </summary>
        [JsonProperty("createDate")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Represents last update time.
        /// </summary>
        [JsonProperty("lastModifiedDate")]
        public DateTime LastModifiedDate { get; set; }

        public static string GetID(Guid cuid)
        {
            return $"Id_{cuid}";
        }

        public static string GetPartitionKey(Guid cuid)
        {
            return $"User_{cuid}";
        }

        public static Guid GetCUID(string msaId, string providerType)
        {
            string combinedString = $"{msaId}-{providerType}";
            using (var sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(Encoding.Default.GetBytes(combinedString));
                byte[] truncatedHash = new byte[16];
                Array.Copy(hash, truncatedHash, 16);
                return new Guid(truncatedHash);
            }
        }
    }
}