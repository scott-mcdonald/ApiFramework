// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq;

using ApiFramework.Schema.Annotations;
using ApiFramework.Schema.Configuration;
using ApiFramework.Schema.Configuration.Internal;

namespace ApiFramework.Schema.Conventions.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiAnnotationDiscoverySchemaConvention : IApiSchemaConvention
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiSchemaConvention Implementation
        public void Apply(IApiSchemaBuilder apiSchemaBuilder, ApiConventionSettings apiConventionSettings)
        {
            Contract.Requires(apiSchemaBuilder != null);

            apiConventionSettings = apiConventionSettings ?? ApiConventionSettings.Empty;
            var apiDiscoverySettings = apiConventionSettings.ApiDiscoverySettings;
            var assemblies           = apiDiscoverySettings.Assemblies;
            var clrTypes = assemblies.SelectMany(x => x.GetExportedTypes())
                                     .ToList();

            var apiSchemaConfiguration = (ApiSchemaConfiguration)apiSchemaBuilder;
            var apiPrecedenceStack     = apiSchemaConfiguration.ApiPrecedenceStack;

            foreach (var clrType in clrTypes)
            {
                var apiTypeKind = clrType.GetApiTypeKind();
                switch (apiTypeKind)
                {
                    case ApiTypeKind.Enumeration:
                    {
                        this.HandleApiEnumerationTypeAttribute(apiSchemaConfiguration, apiConventionSettings, apiPrecedenceStack, clrType);
                        break;
                    }

                    case ApiTypeKind.Object:
                    {
                        this.HandleApiObjectTypeAttribute(apiSchemaConfiguration, apiConventionSettings, apiPrecedenceStack, clrType);
                        break;
                    }
                }
            }
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void HandleApiEnumerationTypeAttribute(IApiSchemaBuilder     apiSchemaBuilder,
                                                       ApiConventionSettings apiConventionSettings,
                                                       ApiPrecedenceStack    apiPrecedenceStack,
                                                       Type                  clrType)
        {
            Contract.Requires(apiSchemaBuilder != null);
            Contract.Requires(apiConventionSettings != null);
            Contract.Requires(clrType != null);

            var apiEnumTypeAttribute = (ApiEnumerationTypeAttribute)Attribute.GetCustomAttribute(clrType, typeof(ApiEnumerationTypeAttribute));
            if (apiEnumTypeAttribute == null)
                return;

            if (ClrTypeDiscoveryRules.CanAddApiEnumerationType(clrType) == false)
                return;

            var apiDiscoverySettings                  = apiConventionSettings.ApiDiscoverySettings;
            var apiAnnotationDiscoveryPredicateResult = apiDiscoverySettings.ApiAnnotationDiscoveryPredicate?.Invoke(clrType) ?? true;
            if (apiAnnotationDiscoveryPredicateResult == false)
                return;

            IApiEnumerationTypeBuilder ApiEnumerationTypeConfiguration(IApiEnumerationTypeBuilder apiEnumTypeBuilder)
            {
                apiPrecedenceStack.Push(ApiPrecedenceLevel.Annotation);
                if (String.IsNullOrWhiteSpace(apiEnumTypeAttribute.Name) == false)
                {
                    apiEnumTypeBuilder.HasName(apiEnumTypeAttribute.Name);
                }

                if (String.IsNullOrWhiteSpace(apiEnumTypeAttribute.Description) == false)
                {
                    apiEnumTypeBuilder.HasDescription(apiEnumTypeAttribute.Description);
                }

                apiPrecedenceStack.Pop();

                return apiEnumTypeBuilder;
            }

            apiPrecedenceStack.Push(ApiPrecedenceLevel.Annotation);
            apiSchemaBuilder.ApiEnumerationType(clrType, ApiEnumerationTypeConfiguration);
            apiPrecedenceStack.Pop();
        }

        private void HandleApiObjectTypeAttribute(IApiSchemaBuilder     apiSchemaBuilder,
                                                  ApiConventionSettings apiConventionSettings,
                                                  ApiPrecedenceStack    apiPrecedenceStack,
                                                  Type                  clrType)
        {
            Contract.Requires(apiSchemaBuilder != null);
            Contract.Requires(apiConventionSettings != null);
            Contract.Requires(clrType != null);

            var apiObjectTypeAttribute = (ApiObjectTypeAttribute)Attribute.GetCustomAttribute(clrType, typeof(ApiObjectTypeAttribute));
            if (apiObjectTypeAttribute == null)
                return;

            if (ClrTypeDiscoveryRules.CanAddApiObjectType(clrType) == false)
                return;

            var apiDiscoverySettings                  = apiConventionSettings.ApiDiscoverySettings;
            var apiAnnotationDiscoveryPredicateResult = apiDiscoverySettings.ApiAnnotationDiscoveryPredicate?.Invoke(clrType) ?? true;
            if (apiAnnotationDiscoveryPredicateResult == false)
                return;

            if (apiObjectTypeAttribute.Excluded)
            {
                apiPrecedenceStack.Push(ApiPrecedenceLevel.Annotation);
                apiSchemaBuilder.Exclude(clrType);
                apiPrecedenceStack.Pop();
                return;
            }

            IApiObjectTypeBuilder ApiObjectTypeConfiguration(IApiObjectTypeBuilder apiObjectTypeBuilder)
            {
                apiPrecedenceStack.Push(ApiPrecedenceLevel.Annotation);
                if (String.IsNullOrWhiteSpace(apiObjectTypeAttribute.Name) == false)
                {
                    apiObjectTypeBuilder.HasName(apiObjectTypeAttribute.Name);
                }

                if (String.IsNullOrWhiteSpace(apiObjectTypeAttribute.Description) == false)
                {
                    apiObjectTypeBuilder.HasDescription(apiObjectTypeAttribute.Description);
                }

                apiPrecedenceStack.Pop();

                return apiObjectTypeBuilder;
            }

            apiPrecedenceStack.Push(ApiPrecedenceLevel.Annotation);
            apiSchemaBuilder.ApiObjectType(clrType, ApiObjectTypeConfiguration);
            apiPrecedenceStack.Pop();
        }
        #endregion
    }
}