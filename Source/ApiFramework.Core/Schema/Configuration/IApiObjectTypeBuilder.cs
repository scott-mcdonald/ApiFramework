// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Linq.Expressions;

namespace ApiFramework.Schema.Configuration
{
    /// <inheritdoc cref="IApiTypeBuilder"/>
    /// <summary>
    /// Represents a fluent-style builder of an API object type.
    /// </summary>
    /// <remarks>
    /// API object type configuration created by the fluent-style builder will take precedence over any API object type configuration created by convention.
    /// </remarks>
    public interface IApiObjectTypeBuilder : IApiTypeBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Excludes the API property from the API object type.</summary>
        /// <returns>A fluent-style builder for the API object type.</returns>
        IApiObjectTypeBuilder Exclude(string clrPropertyName);

        /// <summary>Sets the required name of the API object type.</summary>
        /// <param name="apiName">Name to set the API object type with.</param>
        /// <returns>A fluent-style builder for the API object type.</returns>
        IApiObjectTypeBuilder HasName(string apiName);

        /// <summary>Sets the optional description of the API object type.</summary>
        /// <param name="apiDescription">Description to set the API object type with.</param>
        /// <returns>A fluent-style builder for the API object type.</returns>
        IApiObjectTypeBuilder HasDescription(string apiDescription);

        /// <summary>Adds a property on the API object type by the given CLR property that represents the API property.</summary>
        /// <param name="clrPropertyName">The CLR name of the CLR property.</param>
        /// <param name="clrPropertyType">The CLR type of CLR property.</param>
        /// <param name="configuration">Optional API property configuration, can be null.</param>
        /// <returns>A fluent-style builder for the API object type.</returns>
        IApiObjectTypeBuilder ApiProperty(string clrPropertyName, Type clrPropertyType, Func<IApiPropertyBuilder, IApiPropertyBuilder> configuration);

        /// <summary>Sets object identity on the API object type by the given CLR property that represents the API property.</summary>
        /// <param name="clrPropertyName">The CLR name of the CLR property that represents object identity.</param>
        /// <param name="clrPropertyType">The CLR type of CLR property that represents object identity.</param>
        /// <returns>A fluent-style builder for the API object type.</returns>
        IApiObjectTypeBuilder ApiIdentity(string clrPropertyName, Type clrPropertyType);

        /// <summary>Adds a relationship on the API object type by the given CLR property that represents the related API object type.</summary>
        /// <param name="clrPropertyName">The CLR name of the CLR property that represents the relationship.</param>
        /// <param name="clrPropertyType">The CLR type of CLR property that represents the relationship.</param>
        /// <returns>A fluent-style builder for the API object type.</returns>
        IApiObjectTypeBuilder ApiRelationship(string clrPropertyName, Type clrPropertyType);
        #endregion
    }

    /// <inheritdoc cref="IApiObjectTypeBuilder"/>
    /// <typeparam name="TObject">The CLR object type associated to the API object type for CLR serializing/deserializing purposes.</typeparam>
    public interface IApiObjectTypeBuilder<TObject> : IApiObjectTypeBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Excludes the API property by selecting the CLR property that represents the API property to exclude.</summary>
        /// <returns>A fluent-style builder for the API object type.</returns>
        IApiObjectTypeBuilder<TObject> Exclude<TProperty>(Expression<Func<TObject, TProperty>> clrPropertySelector);

        new IApiObjectTypeBuilder<TObject> HasName(string apiName);

        new IApiObjectTypeBuilder<TObject> HasDescription(string apiDescription);

        /// <summary>Adds a property on the API object type by selecting the CLR property that represents the API property.</summary>
        /// <typeparam name="TProperty">The CLR type of property selected on the CLR object type.</typeparam>
        /// <param name="clrPropertySelector">Expression that selects the CLR property on the CLR object type.</param>
        /// <param name="configuration">Optional API property configuration, can be null.</param>
        /// <returns>A fluent-style builder for the API object type.</returns>
        IApiObjectTypeBuilder<TObject> ApiProperty<TProperty>(Expression<Func<TObject, TProperty>> clrPropertySelector,
                                                              Func<IApiPropertyBuilder, IApiPropertyBuilder> configuration);

        /// <summary>Sets object identity on the API object type by selecting the CLR property that represents the API property.</summary>
        /// <typeparam name="TProperty">The CLR type of property selected on the CLR object type.</typeparam>
        /// <param name="clrPropertySelector">Expression that selects the CLR property on the CLR object type.</param>
        /// <returns>A fluent-style builder for the API object type.</returns>
        IApiObjectTypeBuilder<TObject> ApiIdentity<TProperty>(Expression<Func<TObject, TProperty>> clrPropertySelector);

        /// <summary>Adds a relationship on the API object type by selecting the CLR property that represents the related API object type.</summary>
        /// <typeparam name="TProperty">The CLR type of property selected on the CLR object type.</typeparam>
        /// <param name="clrPropertySelector">Expression that selects the CLR property on the CLR object type.</param>
        /// <returns>A fluent-style builder for the API object type.</returns>
        IApiObjectTypeBuilder<TObject> ApiRelationship<TProperty>(Expression<Func<TObject, TProperty>> clrPropertySelector);
        #endregion
    }
}
