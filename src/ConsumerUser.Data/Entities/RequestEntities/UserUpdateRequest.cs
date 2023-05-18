// <copyright file="UserUpdateRequest.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.CMD.Service.ConsumerUser.Data.Entities.RequestEntities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.CMD.Service.ConsumerUser.Data.Entities;
    using Microsoft.CMD.Service.ConsumerUser.Data.Enums;
    using Newtonsoft.Json;

    public class UserUpdateRequest
    {
        [JsonProperty("msaid")]
        public string MSAID { get; set; }

        [JsonProperty("providerType")]
        public string ProviderType { get; set; }

        [JsonProperty("status")]
        public UserStatus Status { get; set; }
    }
}
