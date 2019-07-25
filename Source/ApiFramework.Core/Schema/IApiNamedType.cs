// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace ApiFramework.Schema
{
    /// <summary>Represents metadata for API types that have a unique name with optional description.</summary>
    public interface IApiNamedType : IApiType
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the API type name.</summary>
        /// <remarks>Must be unique among all API type names on the containing API schema.</remarks>
        string ApiName { get; }

        /// <summary>Gets the optional API type description.</summary>
        string ApiDescription { get; }

        /// <summary>Gets the CLR type associated to the API named type.</summary>
        Type ClrType { get; }
        #endregion
    }
}
