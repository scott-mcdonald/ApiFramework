// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using ApiFramework.Schema.Configuration;

namespace ApiFramework.Schema.Conventions.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiPropertyDiscoveryObjectTypeConvention : IApiObjectTypeConvention
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiObjectTypeConvention Implementation
        public void Apply(IApiObjectTypeBuilder apiObjectTypeBuilder, ApiConventionSettings apiConventionSettings)
        {
            Contract.Requires(apiObjectTypeBuilder != null);

            // Call ApiProperty method on all the discoverable CLR properties for the given CLR object type.
            var clrObjectType = apiObjectTypeBuilder.ClrType;
            var clrProperties = ClrPropertyDiscoveryRules.GetClrProperties(clrObjectType);

            foreach (var clrProperty in clrProperties)
            {
                var clrPropertyName = clrProperty.Name;
                var clrPropertyType = clrProperty.PropertyType;

                apiObjectTypeBuilder.ApiProperty(clrPropertyName, clrPropertyType);
            }
        }
        #endregion
    }
}