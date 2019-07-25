// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using ApiFramework.Schema.Configuration;

namespace ApiFramework.Schema.Conventions
{
    /// <summary>Represents an API object type convention to apply when building API object types.</summary>
    public interface IApiObjectTypeConvention
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Apply the API object type convention on an API object type builder object.</summary>
        /// <param name="apiObjectTypeBuilder">The API object type builder to apply the API object type convention to.</param>
        /// <param name="apiConventionSettings">Optional API convention settings used to build and create an API schema, can be null.</param>
        void Apply(IApiObjectTypeBuilder apiObjectTypeBuilder, ApiConventionSettings apiConventionSettings);
        #endregion
    }
}
