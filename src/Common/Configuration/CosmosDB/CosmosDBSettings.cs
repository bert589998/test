// <copyright file="CosmosDBSettings.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.CMD.Service.ConsumerUser.Common.Utils.CosmosDB
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// CosmosDB settings used by CosmosDBInitializer
    /// Note that some properties are used by other components. Those are not listed here.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class CosmosDBSettings
    {
        public string AccountName { get; set; }

        public string DocumentDbEndpoint { get; set; }
    }
}
