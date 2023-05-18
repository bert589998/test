// -----------------------------------------------------------------------
// <copyright file="UserOnboardRequest.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.CMD.Service.ConsumerUser.Data.Entities.RequestEntities
{
    using Microsoft.CMD.Service.ConsumerUser.Data.Enums;
    using Newtonsoft.Json;

    public class UserOnboardRequest
    {
        [JsonProperty("msaid")]
        public string MSAID { get; set; }

        [JsonProperty("providerType")]
        public string? ProviderType { get; set; }

        [JsonProperty("groupId")]
        public string GroupId { get; set; }
    }
}
