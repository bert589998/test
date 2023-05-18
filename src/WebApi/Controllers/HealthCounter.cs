// <copyright file="HealthCounter.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.CMD.Service.ConsumerUser.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.CMD.Service.ConsumerUser.Common.CosmosDB.Repositories;
    using Microsoft.Management.Services.CloudPC.Instrumentation;

    [Route("api/[controller]/{name}")]
    [ApiController]
    public class HealthCounter
    {
        private readonly ILoggerX logger;
        private readonly IHealthCheckRepository healthCheckRepository;

        // Auto-wired singletons from Startup
        public HealthCounter(ILoggerX logger, IHealthCheckRepository healthCheckRepository)
        {
            this.logger = logger;
            this.healthCheckRepository = healthCheckRepository;
        }

        [HttpGet(Name = "GetHealthCheckCount")]
        public async Task<int> GetAsync(string name)
        {
            var item = await healthCheckRepository.GetOrCreateEntity(name);
            return item.Count;
        }

        [HttpPost(Name = "PostHealthCheckCount")]
        public async Task<int> PostAsync(string name)
        {
            var item = await healthCheckRepository.GetOrCreateEntity(name);
            item.Count += 1;
            await healthCheckRepository.PutEntity(item);
            return item.Count;
        }

        [HttpDelete(Name = "DeleteHealthCheckCount")]
        public async Task<int> DeleteAsync(string name)
        {
            var item = await healthCheckRepository.GetOrCreateEntity(name);
            item.Count = 0;
            await healthCheckRepository.PutEntity(item);
            return item.Count;
        }
    }
}
