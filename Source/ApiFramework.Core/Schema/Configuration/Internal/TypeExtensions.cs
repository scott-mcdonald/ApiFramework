// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using ApiFramework.Reflection;

namespace ApiFramework.Schema.Configuration.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal static class TypeExtensions
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Extension Methods
        public static string CreateDefaultApiEnumerationTypeDescription(this Type clrType)
        {
            Contract.Requires(clrType != null);

            var apiDescription = $".NET {clrType.Name} enumeration type.";
            return apiDescription;
        }

        public static string CreateDefaultApiScalarTypeDescription(this Type clrType)
        {
            Contract.Requires(clrType != null);

            var apiDescription = $".NET {clrType.Name} scalar type.";
            return apiDescription;
        }

        public static ApiTypeKind GetApiTypeKind(this Type clrType)
        {
            // ReSharper disable once UnusedVariable
            return clrType.GetApiTypeKind(out var clrItemType);
        }

        public static ApiTypeKind GetApiTypeKind(this Type clrType, out Type clrItemType)
        {
            clrItemType = null;

            if (clrType.IsClrNullableType())
            {
                var clrNullableUnderlyingType = Nullable.GetUnderlyingType(clrType);

                if (clrNullableUnderlyingType.IsClrEnumType())
                    return ApiTypeKind.Enumeration;

                if (clrNullableUnderlyingType.IsClrScalarType())
                    return ApiTypeKind.Scalar;

                // ReSharper disable once PossibleNullReferenceException
                var message1 = $"Unable to determine the API type kind for a CLR nullable underlying type [name={clrNullableUnderlyingType.Name}]";
                throw new InvalidOperationException(message1);
            }

            if (clrType.IsClrScalarType())
                return ApiTypeKind.Scalar;

            if (clrType.IsClrEnumType())
                return ApiTypeKind.Enumeration;

            if (clrType.IsClrCollectionType(out clrItemType))
                return ApiTypeKind.Collection;

            // Must be last.
            if (clrType.IsClrObjectType())
                return ApiTypeKind.Object;

            var message2 = $"Unable to determine the API type kind for a CLR type [name={clrType.Name}]";
            throw new InvalidOperationException(message2);
        }

        public static Type GetClrLeafType(this Type clrType)
        {
            var apiTypeKind = clrType.GetApiTypeKind(out var clrItemType);
            switch (apiTypeKind)
            {
                case ApiTypeKind.Enumeration:
                case ApiTypeKind.Object:
                case ApiTypeKind.Scalar:
                    return clrType;

                case ApiTypeKind.Collection:
                    return clrItemType.GetClrLeafType();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static bool IsClrCollectionType(this Type clrType, out Type clrItemType)
        {
            Contract.Requires(clrType != null);

            return TypeReflection.IsEnumerableOfT(clrType, out clrItemType);
        }

        public static bool IsClrEnumType(this Type clrType)
        {
            Contract.Requires(clrType != null);

            return TypeReflection.IsEnum(clrType);
        }

        public static bool IsClrNullableType(this Type clrType)
        {
            Contract.Requires(clrType != null);

            return TypeReflection.IsNullableType(clrType);
        }

        public static bool IsClrObjectType(this Type clrType)
        {
            Contract.Requires(clrType != null);

            return TypeReflection.IsComplex(clrType);
        }

        public static bool IsClrScalarType(this Type clrType)
        {
            Contract.Requires(clrType != null);

            return TypeReflection.IsSimple(clrType) && !TypeReflection.IsEnum(clrType);
        }
        #endregion
    }
}