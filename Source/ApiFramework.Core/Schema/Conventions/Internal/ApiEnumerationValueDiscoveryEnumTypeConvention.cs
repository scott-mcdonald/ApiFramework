// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using ApiFramework.Schema.Configuration;

namespace ApiFramework.Schema.Conventions.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiEnumerationValueDiscoveryEnumTypeConvention : IApiEnumerationTypeConvention
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiObjectTypeConvention Implementation
        public void Apply(IApiEnumerationTypeBuilder apiEnumerationTypeBuilder, ApiConventionSettings apiConventionSettings)
        {
            Contract.Requires(apiEnumerationTypeBuilder != null);

            // Call ApiEnumerationValue method on all the discoverable CLR enum values for the given CLR enum type.
            var clrEnumerationType                = apiEnumerationTypeBuilder.ClrType;
            var clrEnumerationFieldInfoCollection = ClrEnumerationValueDiscoveryRules.GetClrEnumerationValues(clrEnumerationType);
            foreach (var clrFieldInfo in clrEnumerationFieldInfoCollection)
            {
                var clrName    = clrFieldInfo.Name;
                var clrOrdinal = (int)Enum.Parse(clrEnumerationType, clrName);

                apiEnumerationTypeBuilder.ApiEnumerationValue(clrName, clrOrdinal);
            }
        }
        #endregion
    }
}