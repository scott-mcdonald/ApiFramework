// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace ApiFramework.Schema.Annotations
{
    /// <summary>Represents the API enumeration value that is a part of an API enumeration type.</summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ApiEnumerationValueAttribute : Attribute
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets or sets the API name of the API enumeration value.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets the API description of the API enumeration value.</summary>
        public string Description { get; set; }
        #endregion
    }
}