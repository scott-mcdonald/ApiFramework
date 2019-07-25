// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema
{
    /// <summary>Represents the relationship cardinality from an API object type to a related API object type.</summary>
    public enum ApiRelationshipCardinality
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Represents a to-one relationship.</summary>
        ToOne = 1,

        /// <summary>Represents a to-many relationship.</summary>
        ToMany = 2
        #endregion
    }
}
