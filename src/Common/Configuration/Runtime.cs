// <copyright file="Runtime.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.CMD.Service.ConsumerUser.Common.Configuration
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class Runtime
    {
        public bool UseKeyVault { get; set; }

        public string Environment { get; set; }

        public string ScaleUnit { get; set; }

        public string Region { get; set; }

        public string ServiceName { get; set; }
    }
}
