// -----------------------------------------------------------------------
// <copyright file="UserService.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.CMD.Service.ConsumerUser.Component.User
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.CMD.Service.ConsumerUser.Component.Repositories;
    using Microsoft.CMD.Service.ConsumerUser.Data.Entities.DataEntities;
    using Microsoft.CMD.Service.ConsumerUser.Data.Entities.RequestEntities;
    using Microsoft.CMD.Service.ConsumerUser.Data.Entities.ResponseEntities;
    using Microsoft.CMD.Service.ConsumerUser.Data.Enums;
    using Microsoft.Graph;
    using Microsoft.Management.Services.CloudPC.Api.Throttling.Executor;
    using Microsoft.Management.Services.CloudPC.Instrumentation;
    using Microsoft.Management.Services.CloudPC.Storage.CosmosDB.Entities;

    [ExcludeFromCodeCoverage]
    public class UserService : IUserService
    {
        private readonly ILoggerX logger;
        private readonly IUserRepository userRepository;
        private readonly IMSAUserRepository msaUserRepository;

        public UserService(ILoggerX logger, IUserRepository userRepository, IMSAUserRepository msaUserRepository)
        {
            this.logger = logger;
            this.userRepository = userRepository;
            this.msaUserRepository = msaUserRepository;
        }

        public async Task<bool> CreateOrUpdateUserAsync(UserRequestEntity userReq)
        {
            UserDataEntity userDataEntity = new UserDataEntity(userReq.MSAID, userReq.ProviderType);
            if (userReq.Status > 0)
            {
                userDataEntity.Status = userReq.Status;
            }

            UserDataEntity oldUser = await this.userRepository.GetUserAsync(userDataEntity.CUID);

            if (oldUser == null)
            {
                await this.userRepository.UpdateUserAsync(userDataEntity);
            }
            else
            {
                oldUser.LastModifiedDate = DateTime.UtcNow;
                oldUser.Status = userReq.Status;
                await this.userRepository.UpdateUserAsync(oldUser);
            }

            return true;
        }

        public async Task<bool> DeleteUserAsync(UserRequestEntity userReq)
        {
            if (userReq.CUID != default)
            {
                await this.userRepository.DeleteUserAsync(userReq.CUID);
            }
            else if (userReq.MSAID != default && userReq.ProviderType != default)
            {
                await this.userRepository.DeleteUserAsync(UserDataEntity.GetCUID(userReq.MSAID, userReq.ProviderType));
            }

            return true;
        }

        public async Task<UserDataEntity> OnboardUserAsync(UserOnboardRequest userOnboardRequest)
        {
            try
            {
                _ = userOnboardRequest ?? throw new ArgumentNullException(nameof(userOnboardRequest));

                var msaid = userOnboardRequest.MSAID;
                var providerType = userOnboardRequest.ProviderType;
                var groupId = userOnboardRequest.GroupId;

                // _ = providerType ?? throw new ArgumentNullException(nameof(providerType));
                _ = msaid ?? throw new ArgumentNullException(nameof(msaid));
                _ = groupId ?? throw new ArgumentNullException(nameof(groupId));

                var record = await this.msaUserRepository.GetMSAUserAsync(msaid, providerType);
                UserDataEntity user = new UserDataEntity();
                if (record == null)
                {
                    user = new UserDataEntity(msaid, providerType);
                    user.GroupIds.Add(groupId);
                    MSAUserDataEntity msaUser = new MSAUserDataEntity(msaid, providerType);
                    await this.userRepository.CreateUserAsync(user);
                    await this.msaUserRepository.CreateMSAUserAsync(msaUser);
                }
                else
                {
                    user = await this.userRepository.GetUserAsync(record.CUID);
                    user.GroupIds.Add(groupId);
                    user.LastModifiedDate = DateTime.UtcNow;
                    await this.userRepository.UpdateUserAsync(user);
                }

                return user;
            }
            catch (Exception ex)
            {
                logger.LogException("UserService", "OnboardUserAsync", ex);
                throw;
            }
        }

        public async Task<UserDataEntity> GetUserAsync(Guid cuid)
        {
            try
            {
                if (cuid == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(cuid));
                }

                var user = await this.userRepository.GetUserAsync(cuid);

                if (user == null)
                {
                    throw new KeyNotFoundException($"No user found with cuid {cuid}.");
                }

                return user;
            }
            catch (Exception ex)
            {
                logger.LogException("UserService", "GetUserAsync", ex);
                throw;
            }
        }

        public async Task UpdateUserAsync(UserUpdateRequest userUpdateRequest)
        {
            try
            {
                _ = userUpdateRequest ?? throw new ArgumentNullException(nameof(userUpdateRequest));

                var msaid = userUpdateRequest.MSAID;
                var status = userUpdateRequest.Status;
                var providerType = userUpdateRequest.ProviderType;

                if (string.IsNullOrWhiteSpace(msaid))
                {
                    throw new ArgumentNullException(nameof(msaid));
                }

                if (status <= 0 || status >= UserStatus.UnknownFutureValue)
                {
                    throw new ArgumentException($"Status {status} is not valid.");
                }

                Guid cuid = UserDataEntity.GetCUID(msaid, providerType);

                var user = await this.userRepository.GetUserAsync(cuid);

                if (user == null)
                {
                    throw new KeyNotFoundException($"No user found with msaid {msaid} and providerType {providerType}.");
                }

                user.Status = status;
                user.LastModifiedDate = DateTime.UtcNow;

                await this.userRepository.UpdateUserAsync(user);
            }
            catch (Exception ex)
            {
                logger.LogException("UserService", "UpdateUserAsync", ex);
                throw;
            }
        }

        public async Task<UserDataEntity> GetUserByMSAIDAsync(string msaid)
        {
            try
            {
                _ = msaid ?? throw new ArgumentNullException(nameof(msaid));

                var msaResponse = await this.msaUserRepository.GetMSAUserAsync(msaid);
                _ = msaResponse ?? throw new KeyNotFoundException($"No user found with msaid {msaid}");

                return await this.GetUserAsync(msaResponse.CUID);
            }
            catch (Exception ex)
            {
                logger.LogException("UserService", "GetUserInfoByMSAIDAsync", ex);
                throw;
            }
        }
    }
}
