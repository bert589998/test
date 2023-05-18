// <copyright file="IUserRepository.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.CMD.Service.ConsumerUser.Component.Repositories
{
    using Microsoft.CMD.Service.ConsumerUser.Common.Configuration.CosmosDB;
    using Microsoft.CMD.Service.ConsumerUser.Data.Entities.DataEntities;
    using Microsoft.Management.Services.CloudPC.Instrumentation;
    using Microsoft.Management.Services.CloudPC.Storage.CosmosDB.Artifacts;
    using Microsoft.Management.Services.CloudPC.Storage.CosmosDB.Interface;

    /// <summary>
    /// A sample repository.
    /// An interface to access a CosmosDB collection.
    /// ASP.Net provides an IStorage singleton to access CosmosDB in a lower level.
    /// </summary>
    public interface IUserRepository
    {
        Task<UserDataEntity> GetUserAsync(Guid cuid);

        Task CreateUserAsync(UserDataEntity user);

        Task UpdateUserAsync(UserDataEntity user);

        Task DeleteUserAsync(Guid cuid);
    }
}
