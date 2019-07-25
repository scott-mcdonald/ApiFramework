// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Reflection;

using ApiFramework.Schema.Annotations;
using ApiFramework.Schema.Configuration;
using ApiFramework.Schema.Configuration.Internal;

namespace ApiFramework.Schema.Conventions.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiAnnotationDiscoveryObjectTypeConvention : IApiObjectTypeConvention
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiObjectTypeConvention Implementation
        public void Apply(IApiObjectTypeBuilder apiObjectTypeBuilder, ApiConventionSettings apiConventionSettings)
        {
            Contract.Requires(apiObjectTypeBuilder != null);

            var clrObjectType             = apiObjectTypeBuilder.ClrType;
            var clrPropertyInfoCollection = ClrPropertyDiscoveryRules.GetClrProperties(clrObjectType);

            var apiObjectTypeConfiguration = (ApiObjectTypeConfiguration)apiObjectTypeBuilder;
            var apiPrecedenceStack         = apiObjectTypeConfiguration.ApiPrecedenceStack;

            foreach (var clrPropertyInfo in clrPropertyInfoCollection)
            {
                HandleApiPropertyAttribute(apiObjectTypeBuilder, apiPrecedenceStack, clrPropertyInfo);
                HandleApiIdentityAttribute(apiObjectTypeBuilder, apiPrecedenceStack, clrPropertyInfo);
                HandleApiRelationshipAttribute(apiObjectTypeBuilder, apiPrecedenceStack, clrPropertyInfo);
            }
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static void HandleApiPropertyAttribute(IApiObjectTypeBuilder apiObjectTypeBuilder,
                                                       ApiPrecedenceStack    apiPrecedenceStack,
                                                       PropertyInfo          clrPropertyInfo)
        {
            Contract.Requires(apiObjectTypeBuilder != null);
            Contract.Requires(apiPrecedenceStack != null);
            Contract.Requires(clrPropertyInfo != null);

            var apiPropertyAttribute = (ApiPropertyAttribute)Attribute.GetCustomAttribute(clrPropertyInfo, typeof(ApiPropertyAttribute));
            if (apiPropertyAttribute == null)
                return;

            var clrPropertyName = clrPropertyInfo.Name;
            var clrPropertyType = clrPropertyInfo.PropertyType;

            if (apiPropertyAttribute.Excluded)
            {
                apiPrecedenceStack.Push(ApiPrecedenceLevel.Annotation);
                apiObjectTypeBuilder.Exclude(clrPropertyName);
                apiPrecedenceStack.Pop();
                return;
            }

            IApiPropertyBuilder ApiPropertyConfiguration(IApiPropertyBuilder apiPropertyBuilder)
            {
                apiPrecedenceStack.Push(ApiPrecedenceLevel.Annotation);
                if (String.IsNullOrWhiteSpace(apiPropertyAttribute.Name) == false)
                {
                    apiPropertyBuilder.HasName(apiPropertyAttribute.Name);
                }

                if (String.IsNullOrWhiteSpace(apiPropertyAttribute.Description) == false)
                {
                    apiPropertyBuilder.HasDescription(apiPropertyAttribute.Description);
                }

                if (apiPropertyAttribute.Required)
                {
                    apiPropertyBuilder.IsRequired();
                }

                // ReSharper disable once InvertIf
                if (apiPropertyAttribute.ItemRequired)
                {
                    IApiCollectionTypeBuilder ApiCollectionTypeConfiguration(IApiCollectionTypeBuilder apiCollectionTypeBuilder)
                    {
                        apiPrecedenceStack.Push(ApiPrecedenceLevel.Annotation);
                        apiCollectionTypeBuilder.ItemIsRequired();
                        apiPrecedenceStack.Pop();
                        return apiCollectionTypeBuilder;
                    }

                    apiPropertyBuilder.ApiCollectionType(ApiCollectionTypeConfiguration);
                }

                apiPrecedenceStack.Pop();

                return apiPropertyBuilder;
            }

            apiPrecedenceStack.Push(ApiPrecedenceLevel.Annotation);
            apiObjectTypeBuilder.ApiProperty(clrPropertyName, clrPropertyType, ApiPropertyConfiguration);
            apiPrecedenceStack.Pop();
        }

        private static void HandleApiIdentityAttribute(IApiObjectTypeBuilder apiObjectTypeBuilder,
                                                       ApiPrecedenceStack    apiPrecedenceStack,
                                                       PropertyInfo          clrPropertyInfo)
        {
            Contract.Requires(apiObjectTypeBuilder != null);
            Contract.Requires(clrPropertyInfo != null);

            var apiIdentityAttribute = Attribute.GetCustomAttribute(clrPropertyInfo, typeof(ApiIdentityAttribute));
            if (apiIdentityAttribute == null)
                return;

            var clrPropertyName = clrPropertyInfo.Name;
            var clrPropertyType = clrPropertyInfo.PropertyType;
            apiPrecedenceStack.Push(ApiPrecedenceLevel.Annotation);
            apiObjectTypeBuilder.ApiIdentity(clrPropertyName, clrPropertyType);
            apiPrecedenceStack.Pop();
        }

        private static void HandleApiRelationshipAttribute(IApiObjectTypeBuilder apiObjectTypeBuilder,
                                                           ApiPrecedenceStack    apiPrecedenceStack,
                                                           PropertyInfo          clrPropertyInfo)
        {
            Contract.Requires(apiObjectTypeBuilder != null);
            Contract.Requires(clrPropertyInfo != null);

            var apiRelationshipAttribute = Attribute.GetCustomAttribute(clrPropertyInfo, typeof(ApiRelationshipAttribute));
            if (apiRelationshipAttribute == null)
                return;

            var clrPropertyName = clrPropertyInfo.Name;
            var clrPropertyType = clrPropertyInfo.PropertyType;
            apiPrecedenceStack.Push(ApiPrecedenceLevel.Annotation);
            apiObjectTypeBuilder.ApiRelationship(clrPropertyName, clrPropertyType);
            apiPrecedenceStack.Pop();
        }
        #endregion
    }
}