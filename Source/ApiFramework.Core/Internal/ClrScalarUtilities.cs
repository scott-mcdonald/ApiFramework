// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using ApiFramework.Reflection;

namespace ApiFramework.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal static class ClrScalarUtilities<TScalar>
    {
        // PUBLIC METHODS //////////////////////////////////////////////////
        #region Methods
        public static string Stringify(TScalar clrScalar)
        {
            // Special case: TScalar is the string type.
            if (IsStringType)
            {
                return clrScalar != null ? $"\"{clrScalar}\"" : "null";
            }

            return IsValueType ? clrScalar.ToString() : clrScalar?.ToString() ?? "null";
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Fields
        private static readonly bool IsValueType = TypeReflection.IsValueType(typeof(TScalar));

        private static readonly bool IsStringType = TypeReflection.IsString(typeof(TScalar));
        #endregion
    }
}