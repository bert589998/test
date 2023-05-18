// -----------------------------------------------------------------------
// <copyright file="IHelloWorldService.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.CMD.Service.ConsumerUser.Component.External.Group
{
    using Microsoft.CMD.Service.ConsumerUser.Data.Entities.RequestEntities;

    public interface IHelloWorldService
    {
        Task<bool> AddHelloWorldAsync(HelloRequestEntity helloReq);
    }
}
