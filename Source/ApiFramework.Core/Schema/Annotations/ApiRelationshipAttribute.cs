// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace ApiFramework.Schema.Annotations
{
    /// <summary>Represents the API property that represents an API relationship from the containing API type to another API type.</summary>
    /// <remarks>The relationship cardinality is determined if the property type is an API object type (to-one) or an API collection type (to-many).</remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public class ApiRelationshipAttribute : Attribute
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets or sets if the API relationship is excluded from the containing API type.</summary>
        public bool Excluded { get; set; }
        #endregion
    }
}