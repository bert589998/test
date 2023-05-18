// <copyright file="OCEUserController.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.CMD.Service.ConsumerUser.WebApi.Controllers.Private
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.CMD.Service.ConsumerUser.Component.Repositories;
    using Microsoft.CMD.Service.ConsumerUser.Component.User;
    using Microsoft.CMD.Service.ConsumerUser.Data.Constants.Auth;
    using Microsoft.CMD.Service.ConsumerUser.Data.Entities.RequestEntities;
    using Microsoft.CMD.Service.ConsumerUser.Data.Entities.ResponseEntities;
    using Microsoft.Management.Services.CloudPC.Api;
    using Microsoft.Management.Services.CloudPC.Instrumentation;

    [ApiController]
    [Route("/oce/users/")]
    [ExcludeFromCodeCoverage]
    [Authorize(AuthenticationSchemes = AuthenticationSchemes.ThirdPartyAuthenticationAppId)]
    public class OCEUserController : ControllerBase
    {
        private readonly ILoggerX logger;
        private readonly IUserRepository userRepository;
        private readonly IUserService userService;

        // Auto-wired singletons from Startup
        public OCEUserController(ILoggerX logger, IUserRepository userRepository, IUserService userService)
        {
            this.logger = logger;
            this.userRepository = userRepository;
            this.userService = userService;
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = ConsumerUserServiceRolesConstants.OCEServiceReadWriteRole)]
        public async Task<IActionResult> DeleteUserAsync(UserRequestEntity userReq)
        {
            await this.userService.DeleteUserAsync(userReq);
            return Ok(new UserResponseEntity<UserResponseResult>());
        }

        [HttpPost]
        [Route("update")]
        [Authorize(Roles = ConsumerUserServiceRolesConstants.OCEServiceReadWriteRole)]
        public async Task<IActionResult> CreateOrUpdateUserAsync(UserRequestEntity userReq)
        {
            await this.userService.CreateOrUpdateUserAsync(userReq);
            return Ok(new UserResponseEntity<UserResponseResult>());
        }
    }
}
