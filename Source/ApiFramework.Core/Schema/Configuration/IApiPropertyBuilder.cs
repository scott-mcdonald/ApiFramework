// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace ApiFramework.Schema.Configuration
{
    /// <summary>
    /// Represents a fluent-style builder of an API property.
    /// </summary>
    /// <remarks>
    /// API property configuration created by the fluent-style builder will take precedence over any API property configuration created by convention.
    /// </remarks>
    public interface IApiPropertyBuilder
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Gets the CLR property name associated to the API property.</summary>
        string ClrName { get; }

        /// <summary>Gets the CLR property type associated to the API property.</summary>
        Type ClrType { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Sets the required name of the API property.</summary>
        /// <param name="apiName">Name to set the API property with.</param>
        /// <returns>A fluent-style builder for the API property.</returns>
        IApiPropertyBuilder HasName(string apiName);

        /// <summary>Sets the optional description of the API property.</summary>
        /// <param name="apiDescription">Description to set the API property with.</param>
        /// <returns>A fluent-style builder for the API property.</returns>
        IApiPropertyBuilder HasDescription(string apiDescription);

        /// <summary>Adds the API type modifier that configures the API property to be required (property value is always non null).</summary>
        /// <returns>A fluent-style builder for the API property.</returns>
        IApiPropertyBuilder IsRequired();

        /// <summary>
        /// Treats the API property type as an API collection type for configuration purposes.
        /// </summary>
        /// <param name="configuration">Optional API collection type configuration, can be null.</param>
        /// <returns>A fluent-style builder for the API property.</returns>
        IApiPropertyBuilder ApiCollectionType(Func<IApiCollectionTypeBuilder, IApiCollectionTypeBuilder> configuration);
        #endregion
    }
}
