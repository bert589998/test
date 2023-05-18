// <copyright file="UserResponseResult.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.CMD.Service.ConsumerUser.Data.Entities.ResponseEntities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.CMD.Service.ConsumerUser.Data.Entities.DataEntities;
    using Microsoft.CMD.Service.ConsumerUser.Data.Enums;
    using Microsoft.Management.Services.CloudPC.Storage.CosmosDB.Entities;
    using Newtonsoft.Json;

    public class UserResponseResult
    {
        public UserResponseResult(UserDataEntity user)
        {
            CUID = user.CUID;
            MSAID = user.MSAID;
            CreateDate = user.CreateDate;
            Status = user.Status;
            UPN = user.UPN;
            GroupIds = user.GroupIds;
        }

        public UserResponseResult()
        {
        }

        [JsonProperty("cuid")]
        public Guid CUID { get; set; }

        [JsonProperty("msaid")]
        public string MSAID { get; set; }

        [JsonProperty("createDate")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("status")]
        public UserStatus Status { get; set; }

        [JsonProperty("upn")]
        public string UPN { get; set; }

        [JsonProperty("groupIds")]
        public IList<string> GroupIds { get; set; }
    }
}
