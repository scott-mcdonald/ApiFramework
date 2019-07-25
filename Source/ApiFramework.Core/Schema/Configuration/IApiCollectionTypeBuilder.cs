// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema.Configuration
{
    /// <inheritdoc cref="IApiTypeBuilder"/>
    /// <summary>
    /// Represents a fluent-style builder of an API collection type.
    /// </summary>
    /// <remarks>
    /// API collection type configuration created by the fluent-style builder will take precedence over any API collection type configuration created by convention.
    /// </remarks>
    public interface IApiCollectionTypeBuilder : IApiTypeBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Adds the API type modifier that configures the API collection type item to be required (item is always non null).</summary>
        /// <returns>A fluent-style API collection type builder for the API collection type.</returns>
        IApiCollectionTypeBuilder ItemIsRequired();
        #endregion
    }
}
