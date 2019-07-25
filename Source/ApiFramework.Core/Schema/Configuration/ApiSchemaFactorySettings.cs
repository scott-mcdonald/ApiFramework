// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using ApiFramework.Schema.Conventions;

namespace ApiFramework.Schema.Configuration
{
    /// <summary>Represents a factory that creates an API schema with optional conventions.</summary>
    public class ApiSchemaFactorySettings
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets or sets a set of API conventions used to build and create and API schema, can be null.</summary>
        public IApiConventionSet ApiConventionSet { get; set; }

        /// <summary>Gets or sets convention settings for the set of API conventions used to build and create and API schema, can be null.</summary>
        public ApiConventionSettings ApiConventionSettings { get; set; }
        #endregion
    }
}