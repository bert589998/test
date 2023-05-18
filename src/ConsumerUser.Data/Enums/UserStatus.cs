// -----------------------------------------------------------------------
// <copyright file="UserStatus.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Microsoft.CMD.Service.ConsumerUser.Data.Enums
{
    public enum UserStatus
    {
        /// <summary>
        /// Represents active status.
        /// </summary>
        Active = 1,

        /// <summary>
        /// Represents soft delete status.
        /// </summary>
        SoftDelete = 2,

        /// <summary>
        /// Represents fraud status.
        /// </summary>
        Fraud = 3,

        /// <summary>
        /// Represents an unknown future value.
        /// </summary>
        UnknownFutureValue = 4
    }
}
