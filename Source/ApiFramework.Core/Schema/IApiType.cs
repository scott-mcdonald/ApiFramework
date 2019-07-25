// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema
{
    /// <summary>Represents metadata of all API types.</summary>
    public interface IApiType
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the API type kind.</summary>
        ApiTypeKind ApiTypeKind { get; }
        #endregion
    }
}
