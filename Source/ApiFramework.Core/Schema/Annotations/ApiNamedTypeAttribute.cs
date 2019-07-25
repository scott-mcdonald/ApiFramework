// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace ApiFramework.Schema.Annotations
{
    /// <summary>Represents the commonality of API types that have a name and description as part of an API schema.</summary>
    public abstract class ApiNamedTypeAttribute : Attribute
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets or sets the API name of the API type.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets the API description of the API type.</summary>
        public string Description { get; set; }
        #endregion
    }
}