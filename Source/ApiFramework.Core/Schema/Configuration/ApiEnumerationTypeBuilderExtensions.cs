// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

#pragma warning disable 1573

namespace ApiFramework.Schema.Configuration
{
    public static class ApiEnumerationTypeBuilderExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        /// <summary>
        /// Adds an enum value on the API enumeration type by the given CLR name and value that represents the API enumeration value.
        /// </summary>
        /// <param name="clrName">The CLR name of the CLR enum value</param>
        /// <param name="clrOrdinal">The CLR value of the CLR enum value</param>
        /// <returns>A fluent-style API enumeration type builder for the API enumeration type.</returns>
        public static IApiEnumerationTypeBuilder ApiEnumerationValue(this IApiEnumerationTypeBuilder apiEnumerationTypeBuilder, string clrName, int clrOrdinal)
        {
            Contract.Requires(clrName.SafeHasContent());

            return apiEnumerationTypeBuilder.ApiEnumerationValue(clrName, clrOrdinal, null);
        }

        /// <summary>
        /// Adds an enum value on the API enumeration type by selecting the CLR enum value that represents the API enumeration value.
        /// </summary>
        /// <typeparam name="TEnum">The CLR type of enum to select the CLR enum value on.</typeparam>
        /// <param name="clrEnumValueSelector">Expression that selects the CLR enum value on the CLR enum type.</param>
        /// <returns>A fluent-style API enumeration type builder for the API enumeration type.</returns>
        public static IApiEnumerationTypeBuilder<TEnum> ApiEnumerationValue<TEnum>(this IApiEnumerationTypeBuilder<TEnum> apiEnumerationTypeBuilder, Expression<Func<TEnum>> clrEnumValueSelector)
            where TEnum : Enum
        {
            Contract.Requires(clrEnumValueSelector != null);

            return apiEnumerationTypeBuilder.ApiEnumerationValue(clrEnumValueSelector, null);
        }
        #endregion
    }
}