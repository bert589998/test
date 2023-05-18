// -----------------------------------------------------------------------
// <copyright file="DependencyInjector.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.CMD.Service.ConsumerUser.Component
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.CMD.Service.ConsumerUser.Component.External.Group;
    using Microsoft.CMD.Service.ConsumerUser.Component.Repositories;
    using Microsoft.CMD.Service.ConsumerUser.Component.User;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Management.Services.CloudPC.ResilientClientLibrary.HttpUtil.Auth;
    using Microsoft.Management.Services.CloudPC.ResilientClientLibrary.HttpUtil.Extensions;

    /// <summary>
    /// Dependency injector for business logic services.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class DependencyInjector
    {
        public static void InjectCommon(IServiceCollection services, IConfiguration configurations)
        {
            // Internal services
            services.AddSingleton<IUserService, UserService>();

            // Repositories
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IMSAUserRepository, MSAUserRepository>();

            // External service
            services.AddHttpClient<IHelloWorldService, HelloWorldService>().UseDefaultAuthenticationHttpTransport<IPartnerServiceTokenGenerator>();
        }
    }
}
