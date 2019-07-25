// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema.Configuration
{
    /// <inheritdoc cref="IApiTypeBuilder"/>
    /// <summary>
    /// Represents a fluent-style builder of an API scalar type.
    /// </summary>
    /// <remarks>
    /// API scalar type configuration created by the fluent-style builder will take precedence over any API scalar type configuration created by convention.
    /// </remarks>
    public interface IApiScalarTypeBuilder : IApiTypeBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Sets the required name of the API scalar type.</summary>
        /// <param name="apiName">Name to set the API scalar type with.</param>
        /// <returns>A fluent-style API scalar type builder for the API scalar type.</returns>
        IApiScalarTypeBuilder HasName(string apiName);

        /// <summary>Sets the optional description of the API scalar type.</summary>
        /// <param name="apiDescription">Description to set the API scalar type with.</param>
        /// <returns>A fluent-style API scalar type builder for the API scalar type.</returns>
        IApiScalarTypeBuilder HasDescription(string apiDescription);
        #endregion
    }

    /// <inheritdoc cref="IApiScalarTypeBuilder"/>
    /// <typeparam name="TScalar">The CLR scalar type associated to the API scalar type for CLR serializing/deserializing purposes.</typeparam>
    /// <remarks>Technically this interface is not needed currently, but is being used as a placeholder for possible future expansion similar to <see cref="IApiObjectTypeBuilder{TObject}"/></remarks>
    public interface IApiScalarTypeBuilder<TScalar> : IApiScalarTypeBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        new IApiScalarTypeBuilder<TScalar> HasName(string apiName);

        new IApiScalarTypeBuilder<TScalar> HasDescription(string apiDescription);
        #endregion
    }
}
