// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using ApiFramework.Schema.Configuration;

namespace ApiFramework.Schema.Conventions
{
    /// <summary>Represents an API schema convention to apply when building API enumeration types.</summary>
    public interface IApiSchemaConvention
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Apply the API schema convention on an API schema builder enumeration.</summary>
        /// <param name="apiSchemaBuilder">The API schema builder to apply the API schema convention to.</param>
        /// <param name="apiConventionSettings">Optional API convention settings used to build and create an API schema, can be null.</param>
        void Apply(IApiSchemaBuilder apiSchemaBuilder, ApiConventionSettings apiConventionSettings);
        #endregion
    }
}