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
    internal static class ClrEnumerationValueDiscoveryRules
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Methods
        /// <summary>Standardizes the discovery of CLR enumeration values on a CLR enum type.</summary>
        public static IReadOnlyCollection<FieldInfo> GetClrEnumerationValues(Type clrEnumerationType)
        {
            Contract.Requires(clrEnumerationType != null);

            var clrEnumValues = TypeReflection.GetFields(clrEnumerationType)
                                              .Where(x => x.FieldType.IsEnum)
                                              .ToList();
            return clrEnumValues;
        }
        #endregion
    }
}