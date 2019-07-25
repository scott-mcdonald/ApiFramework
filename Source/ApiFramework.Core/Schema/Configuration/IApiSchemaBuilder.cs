// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace ApiFramework.Schema.Configuration
{
    /// <summary>
    /// Represents a fluent-style builder of an API schema.
    /// </summary>
    /// <remarks>
    /// API schema configuration created by the fluent-style builder will take precedence over any API schema configuration created by convention.
    /// </remarks>
    public interface IApiSchemaBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Excludes the API type from the API schema.</summary>
        /// <param name="clrType">CLR type that represents the API type to exclude.</param>
        /// <returns>A fluent-style API schema builder for the API schema.</returns>
        IApiSchemaBuilder Exclude(Type clrType);

        /// <summary>Sets the name of the API schema.</summary>
        /// <param name="apiName">Name to set the API schema with.</param>
        /// <returns>A fluent-style API schema builder for the API schema.</returns>
        IApiSchemaBuilder HasName(string apiName);

        /// <summary>Adds an API type configuration to the API schema builder.</summary>
        /// <param name="apiTypeConfiguration">The API type configuration to add to the API schema builder.</param>
        /// <returns>A fluent-style API schema builder for the API schema.</returns>
        IApiSchemaBuilder HasConfiguration(ApiTypeConfiguration apiTypeConfiguration);

        /// <summary>Creates or gets an API enumeration type for the API schema builder as needed and applies the optional API enumeration type configuration.</summary>
        /// <param name="clrEnumType">CLR enumeration type</param>
        /// <param name="configuration">Optional API enumeration type configuration, can be null.</param>
        /// <returns>A fluent-style API schema builder for the API schema.</returns>
        IApiSchemaBuilder ApiEnumerationType(Type clrEnumType, Func<IApiEnumerationTypeBuilder, IApiEnumerationTypeBuilder> configuration);

        /// <summary>Creates or gets an API object type for the API schema builder as needed and applies the optional API object type configuration.</summary>
        /// <param name="clrObjectType">CLR object type</param>
        /// <param name="configuration">Optional API object type configuration, can be null.</param>
        /// <returns>A fluent-style API schema builder for the API schema.</returns>
        IApiSchemaBuilder ApiObjectType(Type clrObjectType, Func<IApiObjectTypeBuilder, IApiObjectTypeBuilder> configuration);

        /// <summary>Creates or gets an API scalar type for the API schema builder as needed and applies the optional API scalar type configuration.</summary>
        /// <param name="clrScalarType">CLR scalar type</param>
        /// <param name="configuration">Optional API scalar type configuration, can be null.</param>
        /// <returns>A fluent-style API schema builder for the API schema.</returns>
        IApiSchemaBuilder ApiScalarType(Type clrScalarType, Func<IApiScalarTypeBuilder, IApiScalarTypeBuilder> configuration);

        /// <summary>Creates or gets an API enumeration type for the API schema builder as needed and applies the optional API enumeration type configuration.</summary>
        /// <typeparam name="TEnum">CLR enumeration type</typeparam>
        /// <param name="configuration">Optional API enumeration type configuration, can be null.</param>
        /// <returns>A fluent-style API schema builder for the API schema.</returns>
        IApiSchemaBuilder ApiEnumerationType<TEnum>(Func<IApiEnumerationTypeBuilder<TEnum>, IApiEnumerationTypeBuilder<TEnum>> configuration)
            where TEnum : Enum;

        /// <summary>Creates or gets an API object type for the API schema builder as needed and applies the optional API object type configuration.</summary>
        /// <typeparam name="TObject">CLR object type</typeparam>
        /// <param name="configuration">Optional API object type configuration, can be null.</param>
        /// <returns>A fluent-style API schema builder for the API schema.</returns>
        IApiSchemaBuilder ApiObjectType<TObject>(Func<IApiObjectTypeBuilder<TObject>, IApiObjectTypeBuilder<TObject>> configuration);

        /// <summary>Creates or gets an API scalar type for the API schema builder as needed and applies the optional API scalar type configuration.</summary>
        /// <typeparam name="TScalar">CLR scalar type</typeparam>
        /// <param name="configuration">Optional API scalar type configuration, can be null.</param>
        /// <returns>A fluent-style API schema builder for the API schema.</returns>
        IApiSchemaBuilder ApiScalarType<TScalar>(Func<IApiScalarTypeBuilder<TScalar>, IApiScalarTypeBuilder<TScalar>> configuration);
        #endregion
    }
}
