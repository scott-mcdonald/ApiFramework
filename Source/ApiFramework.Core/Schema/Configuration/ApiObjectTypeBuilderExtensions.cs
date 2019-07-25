// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

#pragma warning disable 1573

namespace ApiFramework.Schema.Configuration
{
    public static class ApiObjectTypeBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        /// <summary>Adds a property on the API object type by the given CLR property that represents the API property.</summary>
        /// <param name="clrPropertyName">The CLR name of the CLR property.</param>
        /// <param name="clrPropertyType">The CLR type of CLR property.</param>
        /// <returns>A fluent-style builder for the API object type.</returns>
        public static IApiObjectTypeBuilder ApiProperty(this IApiObjectTypeBuilder apiObjectTypeBuilder, string clrPropertyName, Type clrPropertyType)
        {
            Contract.Requires(clrPropertyName.SafeHasContent());
            Contract.Requires(clrPropertyType != null);

            return apiObjectTypeBuilder.ApiProperty(clrPropertyName, clrPropertyType, null);
        }

        /// <summary>Adds a property on the API object type by selecting the CLR property that represents the API property.</summary>
        /// <typeparam name="TObject">The CLR object type associated to the API object type for CLR serializing/deserializing purposes.</typeparam>
        /// <typeparam name="TProperty">The CLR type of property selected on the CLR object type.</typeparam>
        /// <param name="clrPropertySelector">Expression that selects the CLR property on the CLR object type.</param>
        /// <returns>A fluent-style builder for the API object type.</returns>
        public static IApiObjectTypeBuilder<TObject> ApiProperty<TObject, TProperty>(this IApiObjectTypeBuilder<TObject> apiObjectTypeBuilder, Expression<Func<TObject, TProperty>> clrPropertySelector)
        {
            Contract.Requires(clrPropertySelector != null);

            return apiObjectTypeBuilder.ApiProperty(clrPropertySelector, null);
        }
        #endregion
    }
}