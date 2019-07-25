// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema
{
    /// <summary>Represents basic guidance about an API type.</summary>
    public enum ApiTypeKind
    {
        #region Values
        /// <summary>Represents the API collection type.</summary>
        Collection,

        /// <summary>Represents the API enumeration type.</summary>
        Enumeration,

        /// <summary>Represents the API object type.</summary>
        Object,

        /// <summary>Represents the API scalar type.</summary>
        Scalar
        #endregion
    }
}
