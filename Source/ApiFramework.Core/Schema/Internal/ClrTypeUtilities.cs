// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Threading;

using ApiFramework.Reflection;

namespace ApiFramework.Schema.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal static class ClrTypeUtilities
    {
        // PUBLIC METHODS //////////////////////////////////////////////////
        #region Methods
        public static Type GetActualType(Type clrType)
        {
            if (!TypeReflection.IsNullableType(clrType))
                return clrType;

            var clrNullableUnderlyingType = Nullable.GetUnderlyingType(clrType);
            return clrNullableUnderlyingType;
        }
        #endregion
    }

    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal static class ClrTypeUtilities<T>
    {
        // PUBLIC METHODS //////////////////////////////////////////////////
        #region Methods
        public static Type GetActualType()
        {
            return !IsNullableType ? typeof(T) : NullableUnderlyingType.Value;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static bool IsNullableType { get; } = TypeReflection.IsNullableType(typeof(T));
        private static bool IsValueType    { get; } = TypeReflection.IsValueType(typeof(T));

        private static Lazy<Type> NullableUnderlyingType { get; } = new Lazy<Type>(() => Nullable.GetUnderlyingType(typeof(T)), LazyThreadSafetyMode.PublicationOnly);
        #endregion
    }
}