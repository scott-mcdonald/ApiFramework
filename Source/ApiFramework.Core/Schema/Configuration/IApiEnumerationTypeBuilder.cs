// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Linq.Expressions;

namespace ApiFramework.Schema.Configuration
{
    /// <inheritdoc cref="IApiTypeBuilder"/>
    /// <summary>
    /// Represents a fluent-style builder of an API enumeration type.
    /// </summary>
    /// <remarks>
    /// API enumeration type configuration created by the fluent-style builder will take precedence over any API enumeration type configuration created by convention.
    /// </remarks>
    public interface IApiEnumerationTypeBuilder : IApiTypeBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Sets the required name of the API enumeration type.</summary>
        /// <param name="apiName">Name to set the API enumeration type with.</param>
        /// <returns>A fluent-style API enumeration type builder for the API enumeration type.</returns>
        IApiEnumerationTypeBuilder HasName(string apiName);

        /// <summary>Sets the optional description of the API enumeration type.</summary>
        /// <param name="apiDescription">Description to set the API enumeration type with.</param>
        /// <returns>A fluent-style API enumeration type builder for the API enumeration type.</returns>
        IApiEnumerationTypeBuilder HasDescription(string apiDescription);

        /// <summary>
        /// Adds an enumeration value on the API enumeration type by the given CLR name and value that represents the API enumeration value.
        /// </summary>
        /// <param name="clrName">The CLR name of the CLR enumeration value</param>
        /// <param name="clrOrdinal">The CLR ordinal of the CLR enumeration value</param>
        /// <param name="configuration">Optional API enumeration value configuration, can be null.</param>
        /// <returns>A fluent-style API enumeration type builder for the API enumeration type.</returns>
        IApiEnumerationTypeBuilder ApiEnumerationValue(string clrName, int clrOrdinal, Func<IApiEnumerationValueBuilder, IApiEnumerationValueBuilder> configuration);
        #endregion
    }

    /// <inheritdoc cref="IApiEnumerationTypeBuilder"/>
    /// <typeparam name="TEnumeration">The CLR enumeration type associated to the API enumeration type for CLR serializing/deserializing purposes.</typeparam>
    /// <remarks>Technically this interface is not needed currently, but is being used as a placeholder for possible future expansion similar to <see cref="IApiObjectTypeBuilder{TObject}"/></remarks>
    public interface IApiEnumerationTypeBuilder<TEnumeration> : IApiEnumerationTypeBuilder
        where TEnumeration : Enum
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        new IApiEnumerationTypeBuilder<TEnumeration> HasName(string apiName);

        new IApiEnumerationTypeBuilder<TEnumeration> HasDescription(string apiDescription);

        /// <summary>
        /// Adds an enumeration value on the API enumeration type by selecting the CLR enumeration value that represents the API enumeration value.
        /// </summary>
        /// <typeparam name="TEnumeration">The CLR type of enumeration to select the CLR enumeration value on.</typeparam>
        /// <param name="clrEnumerationValueSelector">Expression that selects the CLR enumeration value on the CLR enumeration type.</param>
        /// <param name="configuration">Optional API enumeration value configuration, can be null.</param>
        /// <returns>A fluent-style API enumeration type builder for the API enumeration type.</returns>
        IApiEnumerationTypeBuilder<TEnumeration> ApiEnumerationValue(Expression<Func<TEnumeration>>                                 clrEnumerationValueSelector,
                                                                     Func<IApiEnumerationValueBuilder, IApiEnumerationValueBuilder> configuration);
        #endregion
    }
}