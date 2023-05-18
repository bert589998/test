// -----------------------------------------------------------------------
// <copyright file="IUserService.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.CMD.Service.ConsumerUser.Component.User
{
    using Microsoft.CMD.Service.ConsumerUser.Data.Entities.DataEntities;
    using Microsoft.CMD.Service.ConsumerUser.Data.Entities.RequestEntities;
    using Microsoft.CMD.Service.ConsumerUser.Data.Entities.ResponseEntities;

    public interface IUserService
    {
        /// <summary>
        /// delete by userId
        /// </summary>
        /// <param name="userReq">userId</param>
        /// <returns>bool</returns>
        Task<bool> DeleteUserAsync(UserRequestEntity userReq);

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="userReq">userId</param>
        /// <returns>bool</returns>
        Task<bool> CreateOrUpdateUserAsync(UserRequestEntity userReq);

        Task<UserDataEntity> OnboardUserAsync(UserOnboardRequest userOnboardRequest);

        Task<UserDataEntity> GetUserAsync(Guid cuid);

        Task UpdateUserAsync(UserUpdateRequest userUpdateRequest);

        Task<UserDataEntity> GetUserByMSAIDAsync(string msaid);
    }
}
