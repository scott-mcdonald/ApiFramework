// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using ApiFramework.Schema.Configuration;
using ApiFramework.Schema.Configuration.Internal;

namespace ApiFramework.Schema.Conventions.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiRelationshipDiscoveryObjectTypeConvention : IApiObjectTypeConvention
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiObjectTypeConvention Implementation
        public void Apply(IApiObjectTypeBuilder apiObjectTypeBuilder, ApiConventionSettings apiConventionSettings)
        {
            Contract.Requires(apiObjectTypeBuilder != null);

            // Try and discover relationship CLR properties of the CLR object type:
            var clrObjectType = apiObjectTypeBuilder.ClrType;
            var clrProperties = ClrPropertyDiscoveryRules.GetClrProperties(clrObjectType);
            foreach (var clrProperty in clrProperties)
            {
                var clrPropertyType = clrProperty.PropertyType;
                var apiTypeKind     = clrPropertyType.GetApiTypeKind(out var clrItemType);

                switch (apiTypeKind)
                {
                    case ApiTypeKind.Collection:
                    {
                        var apiItemTypeKind = clrItemType.GetApiTypeKind();
                        switch (apiItemTypeKind)
                        {
                            case ApiTypeKind.Object:
                            {
                                var clrPropertyName = clrProperty.Name;
                                apiObjectTypeBuilder.ApiRelationship(clrPropertyName, clrPropertyType);
                                break;
                            }
                        }
                        break;
                    }

                    case ApiTypeKind.Object:
                    {
                        var clrPropertyName = clrProperty.Name;
                        apiObjectTypeBuilder.ApiRelationship(clrPropertyName, clrPropertyType);
                        break;
                    }
                }
            }
        }
        #endregion
    }
}