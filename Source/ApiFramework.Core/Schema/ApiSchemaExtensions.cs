// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using ApiFramework.Exceptions;

#pragma warning disable 1573

namespace ApiFramework.Schema
{
    /// <summary>Extension methods for the <see cref="IApiSchema"/> interface.</summary>
    public static class ApiSchemaExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        /// <summary>Get the API enumeration type by API type name.</summary>
        /// <param name="apiName">API type name to lookup the API enumeration type by.</param>
        /// <returns>The API enumeration type in the API schema, otherwise an exception is thrown.</returns>
        /// <exception cref="ApiSchemaException"></exception>
        public static IApiNamedType GetApiNamedType(this IApiSchema apiSchema, string apiName)
        {
            Contract.Requires(apiSchema != null);
            Contract.Requires(apiName.SafeHasContent());

            if (apiSchema.TryGetApiNamedType(apiName, out var apiNamedType))
                return apiNamedType;

            // Unable to get API named type by the given API name.
            var message = $"Unable to get API named type [apiName={apiName}] in the API schema, API named type was not configured.";
            throw new ApiSchemaException(message);
        }

        /// <summary>Get the API named type by CLR type object.</summary>
        /// <param name="clrType">CLR type object to lookup the API named type by.</param>
        /// <returns>The API named type in the API schema, otherwise an exception is thrown.</returns>
        /// <exception cref="ApiSchemaException"></exception>
        public static IApiNamedType GetApiNamedType(this IApiSchema apiSchema, Type clrType)
        {
            Contract.Requires(apiSchema != null);
            Contract.Requires(clrType != null);

            if (apiSchema.TryGetApiNamedType(clrType, out var apiNamedType))
                return apiNamedType;

            // Unable to get API named type by the given CLR type.
            var message = $"Unable to get API named type [clrType={clrType.Name}] in the API schema, API named type was not configured.";
            throw new ApiSchemaException(message);
        }

        /// <summary>Get the API named type by CLR type object.</summary>
        /// <typeparam name="T">CLR type to lookup the API named type by.</typeparam>
        /// <returns>The API named type in the API schema, otherwise an exception is thrown.</returns>
        /// <exception cref="ApiSchemaException"></exception>
        public static IApiNamedType GetApiNamedType<T>(this IApiSchema apiSchema)
        {
            Contract.Requires(apiSchema != null);

            var clrType = typeof(T);
            return apiSchema.GetApiNamedType(clrType);
        }

        /// <summary>Try and get the API named type by CLR type.</summary>
        /// <typeparam name="TEnumeration">CLR type to lookup the API named type by.</typeparam>
        /// <param name="apiNamedType">The API named type if it exists in the API schema, null otherwise.</param>
        /// <returns>True if API named type exists in the API schema, false otherwise.</returns>
        public static bool TryGetApiNamedType<TEnumeration>(this IApiSchema apiSchema, out IApiNamedType apiNamedType)
        {
            Contract.Requires(apiSchema != null);

            var clrType = typeof(TEnumeration);
            return apiSchema.TryGetApiNamedType(clrType, out apiNamedType);
        }

        /// <summary>Get the API enumeration type by API type name.</summary>
        /// <param name="apiName">API type name to lookup the API enumeration type by.</param>
        /// <returns>The API enumeration type in the API schema, otherwise an exception is thrown.</returns>
        /// <exception cref="ApiSchemaException"></exception>
        public static IApiEnumerationType GetApiEnumerationType(this IApiSchema apiSchema, string apiName)
        {
            Contract.Requires(apiSchema != null);
            Contract.Requires(apiName.SafeHasContent());

            if (apiSchema.TryGetApiEnumerationType(apiName, out var apiEnumerationType))
                return apiEnumerationType;

            // Unable to get API enumeration type by the given API name.
            var message = $"Unable to get API enumeration type [apiName={apiName}] in the API schema, API enumeration type was not configured.";
            throw new ApiSchemaException(message);
        }

        /// <summary>Get the API enumeration type by CLR type object.</summary>
        /// <param name="clrType">CLR type object to lookup the API enumeration type by.</param>
        /// <returns>The API enumeration type in the API schema, otherwise an exception is thrown.</returns>
        /// <exception cref="ApiSchemaException"></exception>
        public static IApiEnumerationType GetApiEnumerationType(this IApiSchema apiSchema, Type clrType)
        {
            Contract.Requires(apiSchema != null);
            Contract.Requires(clrType != null);

            if (apiSchema.TryGetApiEnumerationType(clrType, out var apiEnumerationType))
                return apiEnumerationType;

            // Unable to get API enumeration type by the given CLR type.
            var message = $"Unable to get API enumeration type [clrType={clrType.Name}] in the API schema, API enumeration type was not configured.";
            throw new ApiSchemaException(message);
        }

        /// <summary>Get the API enumeration type by CLR type object.</summary>
        /// <typeparam name="TEnumeration">CLR type to lookup the API enumeration type by.</typeparam>
        /// <returns>The API enumeration type in the API schema, otherwise an exception is thrown.</returns>
        /// <exception cref="ApiSchemaException"></exception>
        public static IApiEnumerationType GetApiEnumerationType<TEnumeration>(this IApiSchema apiSchema)
        {
            Contract.Requires(apiSchema != null);

            var clrType = typeof(TEnumeration);
            return apiSchema.GetApiEnumerationType(clrType);
        }

        /// <summary>Try and get the API enumeration type by CLR type.</summary>
        /// <typeparam name="TEnumeration">CLR type to lookup the API enumeration type by.</typeparam>
        /// <param name="apiEnumerationType">The API enumeration type if it exists in the API schema, null otherwise.</param>
        /// <returns>True if API enumeration type exists in the API schema, false otherwise.</returns>
        public static bool TryGetApiEnumerationType<TEnumeration>(this IApiSchema apiSchema, out IApiEnumerationType apiEnumerationType)
        {
            Contract.Requires(apiSchema != null);

            var clrType = typeof(TEnumeration);
            return apiSchema.TryGetApiEnumerationType(clrType, out apiEnumerationType);
        }

        /// <summary>Get the API object type by API type name.</summary>
        /// <param name="apiName">API type name to lookup the API object type by.</param>
        /// <returns>The API object type in the API schema, otherwise an exception is thrown.</returns>
        /// <exception cref="ApiSchemaException"></exception>
        public static IApiObjectType GetApiObjectType(this IApiSchema apiSchema, string apiName)
        {
            Contract.Requires(apiSchema != null);
            Contract.Requires(apiName.SafeHasContent());

            if (apiSchema.TryGetApiObjectType(apiName, out var apiObjectType))
                return apiObjectType;

            // Unable to get API object type by the given API name.
            var message = $"Unable to get API object type [apiName={apiName}] in the API schema, API object type was not configured.";
            throw new ApiSchemaException(message);
        }

        /// <summary>Get the API object type by CLR type object.</summary>
        /// <param name="clrType">CLR type object to lookup the API object type by.</param>
        /// <returns>The API object type in the API schema, otherwise an exception is thrown.</returns>
        /// <exception cref="ApiSchemaException"></exception>
        public static IApiObjectType GetApiObjectType(this IApiSchema apiSchema, Type clrType)
        {
            Contract.Requires(apiSchema != null);
            Contract.Requires(clrType != null);

            if (apiSchema.TryGetApiObjectType(clrType, out var apiObjectType))
                return apiObjectType;

            // Unable to get API object type by the given CLR type.
            var message = $"Unable to get API object type [clrType={clrType.Name}] in the API schema, API object type was not configured.";
            throw new ApiSchemaException(message);
        }

        /// <summary>Get the API object type by CLR type object.</summary>
        /// <typeparam name="TObject">CLR type to lookup the API object type by.</typeparam>
        /// <returns>The API object type in the API schema, otherwise an exception is thrown.</returns>
        /// <exception cref="ApiSchemaException"></exception>
        public static IApiObjectType GetApiObjectType<TObject>(this IApiSchema apiSchema)
        {
            Contract.Requires(apiSchema != null);

            var clrType = typeof(TObject);
            return apiSchema.GetApiObjectType(clrType);
        }

        /// <summary>Try and get the API object type by CLR type.</summary>
        /// <typeparam name="TObject">CLR type to lookup the API object type by.</typeparam>
        /// <param name="apiObjectType">The API object type if it exists in the API schema, null otherwise.</param>
        /// <returns>True if API object type exists in the API schema, false otherwise.</returns>
        public static bool TryGetApiObjectType<TObject>(this IApiSchema apiSchema, out IApiObjectType apiObjectType)
        {
            Contract.Requires(apiSchema != null);

            var clrType = typeof(TObject);
            return apiSchema.TryGetApiObjectType(clrType, out apiObjectType);
        }

        /// <summary>Get the API resource type by API type name.</summary>
        /// <param name="apiName">API type name to lookup the API resource type by.</param>
        /// <returns>The API resource type in the API schema, otherwise an exception is thrown.</returns>
        /// <exception cref="ApiSchemaException"></exception>
        public static IApiObjectType GetApiResourceType(this IApiSchema apiSchema, string apiName)
        {
            Contract.Requires(apiSchema != null);
            Contract.Requires(apiName.SafeHasContent());

            if (apiSchema.TryGetApiResourceType(apiName, out var apiResourceType))
            {
                return apiResourceType;
            }

            // Unable to get API resource type by the given API name.
            var message = $"Unable to get API resource type [apiName={apiName}] in the API schema, API resource type was not configured or is not an API resource type.";
            throw new ApiSchemaException(message);
        }

        /// <summary>Get the API resource type by CLR resource type object.</summary>
        /// <param name="clrResourceType">CLR resource type object to lookup the API resource type by.</param>
        /// <returns>The API resource type in the API schema, otherwise an exception is thrown.</returns>
        /// <exception cref="ApiSchemaException"></exception>
        public static IApiObjectType GetApiResourceType(this IApiSchema apiSchema, Type clrResourceType)
        {
            Contract.Requires(apiSchema != null);
            Contract.Requires(clrResourceType != null);

            if (apiSchema.TryGetApiResourceType(clrResourceType, out var apiResourceType))
            {
                return apiResourceType;
            }

            // Unable to get API resource type by the given CLR resource type.
            var message = $"Unable to get API resource type [clrResourceType={clrResourceType.Name}] in the API schema, API resource type was not configured or is not an API resource type.";
            throw new ApiSchemaException(message);
        }

        /// <summary>Get the API resource type by CLR resource type object.</summary>
        /// <typeparam name="TResource">CLR resource type object to lookup the API resource type by.</typeparam>
        /// <returns>The API resource type in the API schema, otherwise an exception is thrown.</returns>
        /// <exception cref="ApiSchemaException"></exception>
        public static IApiObjectType GetApiResourceType<TResource>(this IApiSchema apiSchema)
        {
            Contract.Requires(apiSchema != null);

            var clrResourceType = typeof(TResource);
            return apiSchema.GetApiResourceType(clrResourceType);
        }

        /// <summary>Try and get the API resource type by API type name.</summary>
        /// <param name="apiName">API type name to lookup the API resource type by.</param>
        /// <param name="apiResourceType">The API resource type if it exists in the API schema, null otherwise.</param>
        /// <returns>True if API resource type exists in the API schema, false otherwise.</returns>
        public static bool TryGetApiResourceType(this IApiSchema apiSchema, string apiName, out IApiObjectType apiResourceType)
        {
            Contract.Requires(apiSchema != null);
            Contract.Requires(apiName.SafeHasContent());

            apiResourceType = null;

            if (!apiSchema.TryGetApiObjectType(apiName, out var apiObjectType))
            {
                return false;
            }

            var apiObjectTypeKind = apiObjectType.GetApiObjectTypeKind();
            if (apiObjectTypeKind != ApiObjectTypeKind.ResourceType)
            {
                return false;
            }

            apiResourceType = apiObjectType;
            return true;
        }

        /// <summary>Try and get the API resource type by CLR type object.</summary>
        /// <param name="clrResourceType">CLR resource type object to lookup the API resource type by.</param>
        /// <param name="apiResourceType">The API resource type if it exists in the API schema, null otherwise.</param>
        /// <returns>True if API resource type exists in the API schema, false otherwise.</returns>
        /// <remarks>If the CLR type exists but is an API complex object, then this method returns false.</remarks>
        public static bool TryGetApiResourceType(this IApiSchema apiSchema, Type clrResourceType, out IApiObjectType apiResourceType)
        {
            Contract.Requires(apiSchema != null);
            Contract.Requires(clrResourceType != null);

            apiResourceType = null;

            if (!apiSchema.TryGetApiObjectType(clrResourceType, out var apiObjectType))
            {
                return false;
            }

            var apiObjectTypeKind = apiObjectType.GetApiObjectTypeKind();
            if (apiObjectTypeKind != ApiObjectTypeKind.ResourceType)
            {
                return false;
            }

            apiResourceType = apiObjectType;
            return true;
        }

        /// <summary>Try and get the API resource type by CLR type object.</summary>
        /// <typeparam name="TResource">CLR resource type object to lookup the API resource type by.</typeparam>
        /// <param name="apiResourceType">The API resource type if it exists in the API schema, null otherwise.</param>
        /// <returns>True if API resource type exists in the API schema, false otherwise.</returns>
        /// <remarks>If the CLR type exists but is an API complex object, then this method returns false.</remarks>
        public static bool TryGetApiResourceType<TResource>(this IApiSchema apiSchema, out IApiObjectType apiResourceType)
        {
            Contract.Requires(apiSchema != null);

            var clrResourceType = typeof(TResource);
            return apiSchema.TryGetApiResourceType(clrResourceType, out apiResourceType);
        }

        /// <summary>Get the API scalar type by API type name.</summary>
        /// <param name="apiName">API type name to lookup the API scalar type by.</param>
        /// <returns>The API scalar type in the API schema, otherwise an exception is thrown.</returns>
        /// <exception cref="ApiSchemaException"></exception>
        public static IApiScalarType GetApiScalarType(this IApiSchema apiSchema, string apiName)
        {
            Contract.Requires(apiSchema != null);
            Contract.Requires(apiName.SafeHasContent());

            if (apiSchema.TryGetApiScalarType(apiName, out var apiScalarType))
                return apiScalarType;

            // Unable to get API scalar type by the given API name.
            var message = $"Unable to get API scalar type [apiName={apiName}] in the API schema, API scalar type was not configured.";
            throw new ApiSchemaException(message);
        }

        /// <summary>Get the API scalar type by CLR type object.</summary>
        /// <param name="clrType">CLR type object to lookup the API scalar type by.</param>
        /// <returns>The API scalar type in the API schema, otherwise an exception is thrown.</returns>
        /// <exception cref="ApiSchemaException"></exception>
        public static IApiScalarType GetApiScalarType(this IApiSchema apiSchema, Type clrType)
        {
            Contract.Requires(apiSchema != null);
            Contract.Requires(clrType != null);

            if (apiSchema.TryGetApiScalarType(clrType, out var apiScalarType))
                return apiScalarType;

            // Unable to get API scalar type by the given CLR type.
            var message = $"Unable to get API scalar type [clrType={clrType.Name}] in the API schema, API scalar type was not configured.";
            throw new ApiSchemaException(message);
        }

        /// <summary>Get the API scalar type by CLR type object.</summary>
        /// <typeparam name="TScalar">CLR type to lookup the API scalar type by.</typeparam>
        /// <returns>The API scalar type in the API schema, otherwise an exception is thrown.</returns>
        /// <exception cref="ApiSchemaException"></exception>
        public static IApiScalarType GetApiScalarType<TScalar>(this IApiSchema apiSchema)
        {
            Contract.Requires(apiSchema != null);

            var clrType = typeof(TScalar);
            return apiSchema.GetApiScalarType(clrType);
        }

        /// <summary>Try and get the API scalar type by CLR type.</summary>
        /// <typeparam name="TScalar">CLR type to lookup the API scalar type by.</typeparam>
        /// <param name="apiScalarType">The API scalar type if it exists in the API schema, null otherwise.</param>
        /// <returns>True if API scalar type exists in the API schema, false otherwise.</returns>
        public static bool TryGetApiScalarType<TScalar>(this IApiSchema apiSchema, out IApiScalarType apiScalarType)
        {
            Contract.Requires(apiSchema != null);

            var clrType = typeof(TScalar);
            return apiSchema.TryGetApiScalarType(clrType, out apiScalarType);
        }
        #endregion
    }
}