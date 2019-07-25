// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using ApiFramework.Exceptions;

namespace ApiFramework.Schema
{
    /// <summary>Extension methods for the <see cref="IApiEnumerationType"/> interface.</summary>
    public static class ApiEnumTypeExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        /// <summary>Gets the API enumeration value by API name.</summary>
        /// <param name="apiName">API name to lookup the API enumeration value by.</param>
        /// <returns>API enumeration value for the given API name.</returns>
        // ReSharper disable once InvalidXmlDocComment
        public static IApiEnumerationValue GetApiEnumerationValueByApiName(this IApiEnumerationType apiEnumType, string apiName)
        {
            Contract.Requires(apiEnumType != null);
            Contract.Requires(apiName != null);

            if (apiEnumType.TryGetApiEnumerationValueByApiName(apiName, out var apiEnumValue))
                return apiEnumValue;

            // Unable to get API enumeration value by the given API name.
            var message = $"Unable to get API enumeration value [apiName={apiName}] in the API enumeration type [apiName={apiEnumType.ApiName} clrName={apiEnumType.ClrType.Name}].";
            throw new ApiSchemaException(message);
        }

        /// <summary>Gets the API enumeration value by CLR name.</summary>
        /// <param name="clrName">CLR name to lookup the API enumeration value by.</param>
        /// <returns>API enumeration value for the given CLR name.</returns>
        // ReSharper disable once InvalidXmlDocComment
        public static IApiEnumerationValue GetApiEnumerationValueByClrName(this IApiEnumerationType apiEnumType, string clrName)
        {
            Contract.Requires(apiEnumType != null);
            Contract.Requires(clrName != null);

            if (apiEnumType.TryGetApiEnumerationValueByClrName(clrName, out var apiEnumValue))
                return apiEnumValue;

            // Unable to get API enumeration value by the given CLR name.
            var message = $"Unable to get API enumeration value [clrName={clrName}] in the API enumeration type [apiName={apiEnumType.ApiName} clrName={apiEnumType.ClrType.Name}].";
            throw new ApiSchemaException(message);
        }

        /// <summary>Gets the API enumeration value by CLR ordinal.</summary>
        /// <param name="clrOrdinal">CLR ordinal to lookup the API enumeration value by.</param>
        /// <returns>API enumeration value for the given CLR ordinal.</returns>
        /// <exception cref="ApiSchemaException">Is thrown if the API enumeration value is not found by CLR ordinal.</exception>
        // ReSharper disable once InvalidXmlDocComment
        public static IApiEnumerationValue GetApiEnumerationValueByClrOrdinal(this IApiEnumerationType apiEnumType, int clrOrdinal)
        {
            Contract.Requires(apiEnumType != null);

            if (apiEnumType.TryGetApiEnumerationValueByClrOrdinal(clrOrdinal, out var apiEnumValue))
                return apiEnumValue;

            // Unable to get API enumeration value by the given CLR ordinal.
            var message = $"Unable to get API enumeration value [clrOrdinal={clrOrdinal}] in the API enumeration type [apiName={apiEnumType.ApiName} clrName={apiEnumType.ClrType.Name}].";
            throw new ApiSchemaException(message);
        }
        #endregion
    }
}