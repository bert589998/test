// -----------------------------------------------------------------------
// <copyright file="HelloRequestEntity.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.CMD.Service.ConsumerUser.Data.Entities.RequestEntities
{
    using System;
    using Newtonsoft.Json;

    public class HelloRequestEntity
    {
        [JsonProperty("helloId")]
        public Guid HelloId { get; set; }
    }
}
