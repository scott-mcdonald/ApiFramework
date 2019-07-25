// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace ApiFramework.Schema.Configuration
{
    /// <summary>Represents a fluent-style builder common for all API types.</summary>
    public interface IApiTypeBuilder
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the CLR type name associated with the API type.</summary>
        string ClrName { get; }

        /// <summary>Gets the CLR type associated with the API type.</summary>
        Type ClrType { get; }
        #endregion
    }
}
