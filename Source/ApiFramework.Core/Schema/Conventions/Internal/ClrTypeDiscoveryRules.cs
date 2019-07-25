// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using ApiFramework.Reflection;
using ApiFramework.Schema.Configuration;

namespace ApiFramework.Schema.Conventions.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal static class ClrTypeDiscoveryRules
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        public static bool CanAddApiEnumerationType(Type clrType)
        {
            Contract.Requires(clrType != null);

            return true;
        }

        public static bool CanAddApiObjectType(Type clrType)
        {
            Contract.Requires(clrType != null);

            var isAbstract = TypeReflection.IsAbstract(clrType);
            if (isAbstract)
                return false;

            var hasPublicDefaultConstructor = TypeReflection.GetDefaultConstructor(clrType, ReflectionFlags.Public) != null;
            if (!hasPublicDefaultConstructor)
                return false;

            // Skip any derived ApiTypeConfiguration types.
            var isSubclassOfApiTypeConfiguration = TypeReflection.IsSubclassOf(clrType, typeof(ApiTypeConfiguration));
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (isSubclassOfApiTypeConfiguration)
                return false;

            return true;
        }

        public static bool CanAddApiTypeConfiguration(Type clrType)
        {
            Contract.Requires(clrType != null);

            var isAbstract = TypeReflection.IsAbstract(clrType);
            if (isAbstract)
                return false;

            var hasPublicDefaultConstructor = TypeReflection.GetDefaultConstructor(clrType, ReflectionFlags.Public) != null;
            if (!hasPublicDefaultConstructor)
                return false;

            var isSubclassOfApiTypeConfiguration = TypeReflection.IsSubclassOf(clrType, typeof(ApiTypeConfiguration));
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (!isSubclassOfApiTypeConfiguration)
                return false;

            return true;
        }
        #endregion
    }
}