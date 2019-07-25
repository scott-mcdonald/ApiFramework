// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace ApiFramework.Schema
{
    /// <summary>Represents optional modifiers about an API type.</summary>
    [Flags]
    public enum ApiTypeModifiers
    {
        /// <summary>Represents the API type has no modifiers.</summary>
        None = 0,

        /// <summary>Represents the API instance (of API type) is required and will always be non-null.</summary>
        Required = 1,
    }
}