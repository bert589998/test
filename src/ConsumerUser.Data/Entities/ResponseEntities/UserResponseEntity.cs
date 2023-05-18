// -----------------------------------------------------------------------
// <copyright file="UserResponseEntity.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.CMD.Service.ConsumerUser.Data.Entities.ResponseEntities
{
    using Newtonsoft.Json;

    public class UserResponseEntity<T>
    {
        public UserResponseEntity(T result = default, bool success = true, int code = 200, string message = "success")
        {
            Success = success;
            Code = code;
            Message = message;
            Result = result;
        }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("result")]
        public T Result { get; set; }
    }
}
