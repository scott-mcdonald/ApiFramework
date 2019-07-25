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
    internal class ApiIdentityDiscoveryObjectTypeConvention : IApiObjectTypeConvention
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiObjectTypeConvention Implementation
        public void Apply(IApiObjectTypeBuilder apiObjectTypeBuilder, ApiConventionSettings apiConventionSettings)
        {
            Contract.Requires(apiObjectTypeBuilder != null);

            // Try and discover the identity CLR property of the CLR object type:
            var clrObjectType = apiObjectTypeBuilder.ClrType;
            var clrProperties = ClrPropertyDiscoveryRules.GetClrProperties(clrObjectType);

            // 1. By convention, any CLR property named "Id" is the identity CLR property for this CLR object type.
            var clrIdProperty = ClrPropertyDiscoveryRules.GetClrPropertyByName(clrProperties, "Id");
            if (clrIdProperty != null)
            {
                // Call ApiIdentity method on the discovered CLR property.
                var clrIdPropertyName = clrIdProperty.Name;
                var clrIdPropertyType = clrIdProperty.PropertyType;
                apiObjectTypeBuilder.ApiIdentity(clrIdPropertyName, clrIdPropertyType);
                return;
            }

            // 2. By convention, any CLR property named "XXXId" where XXX is the CLR class name is the identity CLR property for this CLR object type.
            var clrClassName = clrObjectType.Name;
            var clrClassNameAndId = $"{clrClassName}Id";
            var clrClassNameAndIdProperty = ClrPropertyDiscoveryRules.GetClrPropertyByName(clrProperties, clrClassNameAndId);
            // ReSharper disable once InvertIf
            if (clrClassNameAndIdProperty != null)
            {
                // Call ApiIdentity method on the discovered CLR property.
                var clrClassNameAndIdPropertyName = clrClassNameAndIdProperty.Name;
                var clrClassNameAndIdPropertyType = clrClassNameAndIdProperty.PropertyType;
                apiObjectTypeBuilder.ApiIdentity(clrClassNameAndIdPropertyName, clrClassNameAndIdPropertyType);
            }
        }
        #endregion
    }
}