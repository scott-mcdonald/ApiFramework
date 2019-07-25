// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

#pragma warning disable 1573

namespace ApiFramework.Schema.Configuration
{
    public static class ApiSchemaBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        /// <summary>Excludes the API type from the API schema.</summary>
        /// <typeparam name="T">CLR type that represents the API type to exclude.</typeparam>
        /// <returns>A fluent-style API schema builder for the API schema.</returns>
        public static IApiSchemaBuilder Exclude<T>(this IApiSchemaBuilder apiSchemaBuilder)
        {
            Contract.Requires(apiSchemaBuilder != null);

            var clrType = typeof(T);
            return apiSchemaBuilder.Exclude(clrType);
        }

        /// <summary>Creates an API enumeration type for the API schema builder if needed.</summary>
        /// <param name="clrEnumType">CLR enumeration type</param>
        /// <returns>A fluent-style API schema builder for the API schema.</returns>
        public static IApiSchemaBuilder ApiEnumerationType(this IApiSchemaBuilder apiSchemaBuilder, Type clrEnumType)
        {
            Contract.Requires(apiSchemaBuilder != null);
            Contract.Requires(clrEnumType != null);

            return apiSchemaBuilder.ApiEnumerationType(clrEnumType, null);
        }

        /// <summary>Creates an API object type for the API schema builder if needed.</summary>
        /// <param name="clrObjectType">CLR object type</param>
        /// <returns>A fluent-style API schema builder for the API schema.</returns>
        public static IApiSchemaBuilder ApiObjectType(this IApiSchemaBuilder apiSchemaBuilder, Type clrObjectType)
        {
            Contract.Requires(apiSchemaBuilder != null);
            Contract.Requires(clrObjectType != null);

            return apiSchemaBuilder.ApiObjectType(clrObjectType, null);
        }

        /// <summary>Creates an API scalar type for the API schema builder if needed.</summary>
        /// <param name="clrScalarType">CLR scalar type</param>
        /// <returns>A fluent-style API schema builder for the API schema.</returns>
        public static IApiSchemaBuilder ApiScalarType(this IApiSchemaBuilder apiSchemaBuilder, Type clrScalarType)
        {
            Contract.Requires(apiSchemaBuilder != null);
            Contract.Requires(clrScalarType != null);

            return apiSchemaBuilder.ApiScalarType(clrScalarType, null);
        }

        /// <summary>Creates an API enumeration type for the API schema builder if needed.</summary>
        /// <typeparam name="TEnum">CLR enumeration type</typeparam>
        /// <returns>A fluent-style API schema builder for the API schema.</returns>
        public static IApiSchemaBuilder ApiEnumerationType<TEnum>(this IApiSchemaBuilder apiSchemaBuilder)
            where TEnum : Enum
        {
            Contract.Requires(apiSchemaBuilder != null);

            return apiSchemaBuilder.ApiEnumerationType<TEnum>(null);
        }

        /// <summary>Creates an API object type for the API schema builder if needed.</summary>
        /// <typeparam name="TObject">CLR object type</typeparam>
        /// <returns>A fluent-style API schema builder for the API schema.</returns>
        public static IApiSchemaBuilder ApiObjectType<TObject>(this IApiSchemaBuilder apiSchemaBuilder)
        {
            Contract.Requires(apiSchemaBuilder != null);

            return apiSchemaBuilder.ApiObjectType<TObject>(null);
        }

        /// <summary>Creates an API scalar type for the API schema builder if needed.</summary>
        /// <typeparam name="TScalar">CLR scalar type</typeparam>
        /// <returns>A fluent-style API schema builder for the API schema.</returns>
        public static IApiSchemaBuilder ApiScalarType<TScalar>(this IApiSchemaBuilder apiSchemaBuilder)
        {
            Contract.Requires(apiSchemaBuilder != null);

            return apiSchemaBuilder.ApiScalarType<TScalar>(null);
        }
        #endregion
    }
}