// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema.Configuration
{
    /// <summary>Represents a factory that creates an API schema with optional conventions.</summary>
    public interface IApiSchemaFactory
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Factory method that creates an API schema with optional creational settings.</summary>
        /// <param name="apiSchemaFactorySettings">Optional set of settings used to build and create an API schema, can be null.</param>
        /// <returns>An API schema instance.</returns>
        IApiSchema Create(ApiSchemaFactorySettings apiSchemaFactorySettings);
        #endregion
    }
}
