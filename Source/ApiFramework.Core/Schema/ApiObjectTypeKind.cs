// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema
{
    /// <summary>Represents basic guidance about an API object type.</summary>
    public enum ApiObjectTypeKind
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Represents an API object type that does not have identity.</summary>
        ComplexType,

        /// <summary>Represents an API object type that has identity.</summary>
        ResourceType,
        #endregion
    }
}
