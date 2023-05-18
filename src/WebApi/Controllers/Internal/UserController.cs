// <copyright file="UserController.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.CMD.Service.ConsumerUser.WebApi.Controllers.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using Bond;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.CMD.Service.ConsumerUser.Common.CosmosDB.Repositories;
    using Microsoft.CMD.Service.ConsumerUser.Component.Repositories;
    using Microsoft.CMD.Service.ConsumerUser.Component.User;
    using Microsoft.CMD.Service.ConsumerUser.Data.Constants.Auth;
    using Microsoft.CMD.Service.ConsumerUser.Data.Entities;
    using Microsoft.CMD.Service.ConsumerUser.Data.Entities.RequestEntities;
    using Microsoft.CMD.Service.ConsumerUser.Data.Entities.ResponseEntities;
    using Microsoft.CMD.Service.ConsumerUser.Data.Enums;
    using Microsoft.IdentityModel.Clients.ActiveDirectory;
    using Microsoft.Management.Services.CloudPC.Api;
    using Microsoft.Management.Services.CloudPC.Api.Throttling.Executor;
    using Microsoft.Management.Services.CloudPC.Instrumentation;

    [ApiController]
    [ExcludeFromCodeCoverage]
    [Produces("application/json")]
    [Route("/internal/users/")]
    [Authorize(AuthenticationSchemes = AuthenticationSchemes.ThirdPartyAuthenticationAppId)]
    public class UserController : ControllerBase
    {
        private readonly ILoggerX logger;
        private readonly IUserService userService;

        // Auto-wired singletons from Startup
        public UserController(ILoggerX logger, IUserService userService)
        {
            this.logger = logger;
            this.userService = userService;
        }

        [HttpGet]
        [Route("get/{cuid}")]
        [Authorize(Roles = ConsumerUserServiceRolesConstants.ConsumerUserServiceReadRole)]
        public async Task<IActionResult> GetUser([FromRoute] Guid cuid)
        {
            try
            {
                var user = await this.userService.GetUserAsync(cuid);
                var response = new UserResponseResult(user);
                return Ok(new UserResponseEntity<UserResponseResult>(response));
            }
            catch (Exception ex)
            {
                logger.LogException("UserController", "GetUser", ex);
                return Ok(new UserResponseEntity<UserResponseResult>() { Success = false, Code = 204, Message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("offboard/{msaId}/{providerType}")]
        [Authorize(Roles = ConsumerUserServiceRolesConstants.ConsumerUserServiceWriteRole)]
        public async Task<IActionResult> DeleteUserAsync(string msaId, string providerType)
        {
            await userService.DeleteUserAsync(new UserRequestEntity() { MSAID = msaId, ProviderType = providerType });
            return Ok(new UserResponseEntity<UserResponseResult>());
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = ConsumerUserServiceRolesConstants.ConsumerUserServiceWriteRole)]
        public async Task<IActionResult> CreateOrUpdateUserAsync(UserRequestEntity userReq)
        {
            await userService.CreateOrUpdateUserAsync(userReq);
            return Ok(new UserResponseEntity<UserResponseResult>());
        }

        [HttpPost]
        [Route("onboard")]
        [Authorize(Roles = ConsumerUserServiceRolesConstants.ConsumerUserServiceWriteRole)]
        public async Task<IActionResult> OnboardUser([FromBody] UserOnboardRequest userOnboardRequest)
        {
            try
            {
                var user = await userService.OnboardUserAsync(userOnboardRequest);
                var response = new UserResponseResult() { MSAID = user.MSAID, CUID = user.CUID };
                return Ok(new UserResponseEntity<UserResponseResult>(response));
            }
            catch (Exception ex)
            {
                logger.LogException("UserController", "OnboardUser", ex);
                var response = new UserResponseResult() { MSAID = userOnboardRequest.MSAID };
                return Ok(new UserResponseEntity<UserResponseResult>(response) { Success = false, Code = 200, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("update")]
        [Authorize(Roles = ConsumerUserServiceRolesConstants.ConsumerUserServiceWriteRole)]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest userUpdateRequest)
        {
            try
            {
                await userService.UpdateUserAsync(userUpdateRequest);
                return Ok(new UserResponseEntity<UserResponseResult>());
            }
            catch (Exception ex)
            {
                logger.LogException("UserController", "UpdateUser", ex);
                return Ok(new UserResponseEntity<UserResponseResult>() { Success = false, Code = 200, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("getByMsaId/{msaid}")]
        public async Task<IActionResult> GetUserByMSAID([FromRoute] string msaid)
        {
            try
            {
                var user = await this.userService.GetUserByMSAIDAsync(msaid);
                var response = new UserResponseResult(user);
                return Ok(new UserResponseEntity<UserResponseResult>(response));
            }
            catch (Exception ex)
            {
                logger.LogException("UserController", "GetUserByMSAID", ex);
                return Ok(new UserResponseEntity<UserResponseResult>() { Success = false, Code = 204, Message = ex.Message });
            }
        }
    }
}
