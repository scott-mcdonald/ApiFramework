// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using ApiFramework.Schema.Configuration;

namespace ApiFramework.Schema.Conventions
{
    /// <summary>Represents an API enumeration type convention to apply when building API enumeration types.</summary>
    public interface IApiEnumerationTypeConvention
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Apply the API enumeration type convention on an API enumeration type builder enumeration.</summary>
        /// <param name="apiEnumerationTypeBuilder">The API enumeration type builder to apply the API enumeration type convention to.</param>
        /// <param name="apiConventionSettings">Optional API convention settings used to build and create an API schema, can be null.</param>
        void Apply(IApiEnumerationTypeBuilder apiEnumerationTypeBuilder, ApiConventionSettings apiConventionSettings);
        #endregion
    }
}