// <copyright file="Health.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

namespace Microsoft.CMD.Service.ConsumerUser.WebApi.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class Health : ControllerBase
    {
        [HttpGet(Name = "GetHealth")]
        public string Get()
        {
            return "Success";
        }
    }
}
