// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using ApiFramework.Exceptions;

namespace ApiFramework.Schema
{
    /// <summary>Extension methods for the <see cref="IApiObjectType"/> interface.</summary>
    public static class ApiObjectTypeExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        /// <summary>
        /// Gets the basic type guidance enumeration for the API object type.
        /// </summary>
        /// <param name="apiObjectType">API object type to call extension method on.</param>
        /// <returns>An <see cref="ApiObjectTypeKind"/> giving basic type guidance about the API object type.</returns>
        public static ApiObjectTypeKind GetApiObjectTypeKind(this IApiObjectType apiObjectType)
        {
            Contract.Requires(apiObjectType != null);

            var apiObjectTypeKind = apiObjectType.ApiIdentity != null ? ApiObjectTypeKind.ResourceType : ApiObjectTypeKind.ComplexType;
            return apiObjectTypeKind;
        }

        /// <summary>Get the API property by API property name, thrown an exception if the API property does not exist.</summary>
        /// <param name="apiObjectType">API object type to call extension method on.</param>
        /// <param name="apiName">API property name to lookup the API property by.</param>
        /// <returns>The named API property if it exists, throws an exception if the API property does not exist.</returns>
        public static IApiProperty GetApiPropertyByApiName(this IApiObjectType apiObjectType, string apiName)
        {
            Contract.Requires(apiObjectType != null);
            Contract.Requires(apiName != null);

            if (apiObjectType.TryGetApiPropertyByApiName(apiName, out var apiProperty))
                return apiProperty;

            // Unable to get API property by the given API property name.
            var message = $"Unable to get API property [apiName={apiName}] in the API object type [apiName={apiObjectType.ApiName} clrName={apiObjectType.ClrType.Name}].";
            throw new ApiSchemaException(message);
        }

        /// <summary>Get the API property by CLR property name, thrown an exception if the API property does not exist.</summary>
        /// <param name="apiObjectType">API object type to call extension method on.</param>
        /// <param name="clrName">CLR property name to lookup the API property by.</param>
        /// <returns>The named API property if it exists, throws an exception if the API property does not exist.</returns>
        public static IApiProperty GetApiPropertyByClrName(this IApiObjectType apiObjectType, string clrName)
        {
            Contract.Requires(apiObjectType != null);
            Contract.Requires(clrName != null);

            if (apiObjectType.TryGetApiPropertyByClrName(clrName, out var apiProperty))
                return apiProperty;

            // Unable to get API property by the given CLR property name.
            var message = $"Unable to get API property [clrName={clrName}] in the API object type [apiName={apiObjectType.ApiName} clrName={apiObjectType.ClrType.Name}].";
            throw new ApiSchemaException(message);
        }

        /// <summary>Get the API relationship by API relationship name, thrown an exception if the API relationship does not exist.</summary>
        /// <param name="apiObjectType">API object type to call extension method on.</param>
        /// <param name="apiName">API relationship name to lookup the API relationship by.</param>
        /// <returns>The named API relationship if it exists, throws an exception if the API relationship does not exist.</returns>
        public static IApiRelationship GetApiRelationshipByApiName(this IApiObjectType apiObjectType, string apiName)
        {
            Contract.Requires(apiObjectType != null);
            Contract.Requires(apiName       != null);

            if (apiObjectType.TryGetApiRelationshipByApiName(apiName, out var apiRelationship))
                return apiRelationship;

            // Unable to get API relationship by the given API relationship name.
            var message = $"Unable to get API relationship [apiName={apiName}] in the API object type [apiName={apiObjectType.ApiName} clrName={apiObjectType.ClrType.Name}].";
            throw new ApiSchemaException(message);
        }

        /// <summary>Get the API relationship by CLR relationship name, thrown an exception if the API relationship does not exist.</summary>
        /// <param name="apiObjectType">API object type to call extension method on.</param>
        /// <param name="clrName">CLR relationship name to lookup the API relationship by.</param>
        /// <returns>The named API relationship if it exists, throws an exception if the API relationship does not exist.</returns>
        public static IApiRelationship GetApiRelationshipByClrName(this IApiObjectType apiObjectType, string clrName)
        {
            Contract.Requires(apiObjectType != null);
            Contract.Requires(clrName       != null);

            if (apiObjectType.TryGetApiRelationshipByClrName(clrName, out var apiRelationship))
                return apiRelationship;

            // Unable to get API relationship by the given CLR relationship name.
            var message = $"Unable to get API relationship [clrName={clrName}] in the API object type [apiName={apiObjectType.ApiName} clrName={apiObjectType.ClrType.Name}].";
            throw new ApiSchemaException(message);
        }
        #endregion
    }
}