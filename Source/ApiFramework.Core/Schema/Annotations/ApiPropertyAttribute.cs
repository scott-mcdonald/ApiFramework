// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace ApiFramework.Schema.Annotations
{
    /// <summary>Represents the API property that is a part of an API type.</summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ApiPropertyAttribute : Attribute
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets or sets the API name of the API property.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets the API description of the API property.</summary>
        public string Description { get; set; }

        /// <summary>Gets or sets if the API property value is required (non-null).</summary>
        public bool Required { get; set; }

        /// <summary>Gets or sets if the API property value collection items are required (non-null)</summary>
        /// <remarks>Only applicable if the API property type is an API collection type.</remarks>
        public bool ItemRequired { get; set; }

        /// <summary>Gets or sets if the API property is excluded from the containing API type.</summary>
        public bool Excluded { get; set; }
        #endregion
    }
}