// <copyright file="IMSAUserRepository.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.CMD.Service.ConsumerUser.Component.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.CMD.Service.ConsumerUser.Data.Entities.DataEntities;

    public interface IMSAUserRepository
    {
        Task<MSAUserDataEntity> GetMSAUserAsync(string msaid, string? providerType = null);

        Task CreateMSAUserAsync(MSAUserDataEntity user);

        Task UpdateMSAUserAsync(MSAUserDataEntity user);

        Task DeleteMSAUserAsync(string msaid, string? providerType = null);
    }
}
