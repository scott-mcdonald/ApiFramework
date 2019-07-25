// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

using ApiFramework.Reflection;

namespace ApiFramework.Schema.Conventions.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal static class ClrPropertyDiscoveryRules
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Standardizes the discovery of CLR properties on a CLR object type.</summary>
        public static IReadOnlyCollection<PropertyInfo> GetClrProperties(Type clrObjectType)
        {
            Contract.Requires(clrObjectType != null);

            // Use reflection, get all the directly declared, public, and instance-based type of properties that have a public getter and setter for the given CLR object type.
            var clrProperties = TypeReflection.GetProperties(clrObjectType, ReflectionFlags.DeclaredOnly | ReflectionFlags.Public | ReflectionFlags.Instance)
                                              .Where(x =>
                                               {
                                                   var hasPublicGetter = x.GetGetMethod(false) != null;
                                                   var hasPublicSetter = x.GetSetMethod(false) != null;
                                                   return hasPublicGetter && hasPublicSetter;
                                               })
                                              .ToList();
            return clrProperties;
        }

        /// <summary>Standardizes the way to search for a CLR property by case-insensitive property name search.</summary>
        public static PropertyInfo GetClrPropertyByName(IEnumerable<PropertyInfo> clrProperties, string clrPropertyName)
        {
            var clrProperty = clrProperties.SingleOrDefault(x => String.Equals(x.Name, clrPropertyName, StringComparison.OrdinalIgnoreCase));
            return clrProperty;
        }
        #endregion
    }
}