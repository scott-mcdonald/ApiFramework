// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace ApiFramework.Schema.Annotations
{
    /// <summary>Represents the API object type that is part of an API schema.</summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ApiObjectTypeAttribute : ApiNamedTypeAttribute
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets or sets if the API object type is excluded from the containing API schema.</summary>
        public bool Excluded { get; set; }
        #endregion
    }
}