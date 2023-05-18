// -----------------------------------------------------------------------
// <copyright file="HelloWorldService.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.CMD.Service.ConsumerUser.Component.External.Group
{
    using System.Threading.Tasks;
    using Microsoft.CMD.Service.ConsumerUser.Data.Entities.RequestEntities;
    using Microsoft.Management.Services.CloudPC.Instrumentation;
    using Microsoft.Management.Services.CloudPC.Instrumentation.Enum;
    using Microsoft.Management.Services.CloudPC.ResilientClientLibrary.HttpUtil;

    public class HelloWorldService : TypedRestClient<HelloWorldService>, IHelloWorldService
    {
        public HelloWorldService(ILoggerX logger, HttpClient httpClient, IServiceMap<PartnerServiceMap> serviceMap)
            : base(
                  logger,
                  httpClient,
                  serviceMap.GetServiceUri(PartnerService.GroupService))
        {
        }

        public async Task<bool> AddHelloWorldAsync(HelloRequestEntity helloReq)
        {
            string uri = $"internal/group/add";
            try
            {
                await this.PostAsync<Task>(uri, helloReq.ToString());
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
