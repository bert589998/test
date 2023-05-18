// <copyright file="Startup.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.CMD.Service.ConsumerUser.WebApi
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.CloudManagedDesktop.Core.Flighting;
    using Microsoft.CMD.Service.ConsumerUser.Common.CosmosDB.Repositories;
    using Microsoft.CMD.Service.ConsumerUser.Common.Utils.CosmosDB;
    using Microsoft.CMD.Service.ConsumerUser.Component;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Management.Services.CloudPC.ResilientClientLibrary.HttpUtil.Auth;
    using Microsoft.Management.Services.CloudPC.ResilientClientLibrary.HttpUtil.Extensions;
    using StartupBase = Microsoft.Management.Services.CloudPC.Api.StartupBase;

    public class Startup : StartupBase
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env) : base(configuration, env)
        {
        }

        protected override void ConfigureDependencies(IServiceCollection services)
        {
            services.AddHttpClient();

            CosmosDBInitializer.SetupDatabase(services, Logger, Configuration);

            services.AddPartnerServiceMap(this.Configuration);

            InitializeCosmosDBRepositories(services);

            DependencyInjector.InjectCommon(services, this.Configuration);

            services.AddFlighting(this.Configuration);
        }

        private void InitializeCosmosDBRepositories(IServiceCollection services)
        {
            services.AddSingleton<IHealthCheckRepository, HealthCheckRepository>();
        }
    }
}
