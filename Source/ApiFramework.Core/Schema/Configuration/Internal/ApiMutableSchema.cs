// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using ApiFramework.Schema.Conventions;
using ApiFramework.Schema.Internal;

namespace ApiFramework.Schema.Configuration.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiMutableSchema
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiMutableSchema(ApiSchemaFactorySettings apiSchemaFactorySettings)
        {
            this.ApiConventionSet      = apiSchemaFactorySettings?.ApiConventionSet;
            this.ApiConventionSettings = apiSchemaFactorySettings?.ApiConventionSettings;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public string ApiName { get; set; }

        public IApiConventionSet ApiConventionSet { get; }

        public ApiConventionSettings ApiConventionSettings { get; }

        public IDictionary<Type, ApiTypeConfiguration> ApiEnumerationTypeConfigurationDictionary { get; } = new Dictionary<Type, ApiTypeConfiguration>();
        public IDictionary<Type, ApiTypeConfiguration> ApiObjectTypeConfigurationDictionary      { get; } = new Dictionary<Type, ApiTypeConfiguration>();
        public IDictionary<Type, ApiTypeConfiguration> ApiScalarTypeConfigurationDictionary      { get; } = new Dictionary<Type, ApiTypeConfiguration>();

        public ISet<Type> ClrExcludedTypes            { get; } = new HashSet<Type>();
        public ISet<Type> ClrExcludedEnumerationTypes { get; } = new HashSet<Type>();
        public ISet<Type> ClrExcludedObjectTypes      { get; } = new HashSet<Type>();
        public ISet<Type> ClrExcludedScalarTypes      { get; } = new HashSet<Type>();

        public ISet<Type> ClrImplicitEnumerationTypes { get; } = new HashSet<Type>();
        public ISet<Type> ClrImplicitScalarTypes      { get; } = new HashSet<Type>();

        public ISet<Type> ClrResourceTypes { get; } = new HashSet<Type>();
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            return $"{nameof(ApiMutableSchema)} [{nameof(this.ApiName)}={this.ApiName}]";
        }
        #endregion

        #region Methods
        public void AddClrExcludedEnumerationType(Type clrType)
        {
            Contract.Requires(clrType != null);

            this.ClrExcludedTypes.Add(clrType);
            this.ClrExcludedEnumerationTypes.Add(clrType);
        }

        public void AddClrExcludedObjectType(Type clrType)
        {
            Contract.Requires(clrType != null);

            this.ClrExcludedTypes.Add(clrType);
            this.ClrExcludedObjectTypes.Add(clrType);
        }

        public void AddClrExcludedScalarType(Type clrType)
        {
            Contract.Requires(clrType != null);

            this.ClrExcludedTypes.Add(clrType);
            this.ClrExcludedScalarTypes.Add(clrType);
        }

        public void AddClrImplicitEnumerationType(Type clrType)
        {
            Contract.Requires(clrType != null);

            var clrActualType = TypeUtilities.GetActualType(clrType);

            this.ClrImplicitEnumerationTypes.Add(clrActualType);
        }

        public void AddClrImplicitScalarType(Type clrType)
        {
            Contract.Requires(clrType != null);

            var clrActualType = TypeUtilities.GetActualType(clrType);

            this.ClrImplicitScalarTypes.Add(clrActualType);
        }

        public void AddClrResourceType(Type clrType)
        {
            Contract.Requires(clrType != null);

            this.ClrResourceTypes.Add(clrType);
        }

        public void AddOrUpdateApiEnumerationTypeConfiguration(ApiTypeConfiguration apiTypeConfiguration,
                                                               ApiPrecedenceLevel   apiPrecedenceLevel,
                                                               ApiPrecedenceStack   apiPrecedenceStack)
        {
            Contract.Requires(apiTypeConfiguration != null);
            Contract.Requires(apiPrecedenceStack != null);

            AddOrUpdateApiTypeConfiguration<ApiEnumerationTypeConfiguration, ApiMutableFactory<ApiMutableSchema, ApiMutableEnumerationType>, ApiMutableEnumerationType>(this.ApiEnumerationTypeConfigurationDictionary, apiTypeConfiguration,
                                                                                                                                                                        ApiEnumerationTypeConfigurationOpenGenericType,
                                                                                                                                                                        apiPrecedenceLevel, apiPrecedenceStack);
        }

        public void AddOrUpdateApiObjectTypeConfiguration(ApiTypeConfiguration apiTypeConfiguration,
                                                          ApiPrecedenceLevel   apiPrecedenceLevel,
                                                          ApiPrecedenceStack   apiPrecedenceStack)
        {
            Contract.Requires(apiTypeConfiguration != null);
            Contract.Requires(apiPrecedenceStack != null);

            AddOrUpdateApiTypeConfiguration<ApiObjectTypeConfiguration, ApiMutableFactory<ApiMutableSchema, ApiMutableObjectType>, ApiMutableObjectType>(this.ApiObjectTypeConfigurationDictionary, apiTypeConfiguration,
                                                                                                                                                         ApiObjectTypeConfigurationOpenGenericType, apiPrecedenceLevel, apiPrecedenceStack);
        }

        public void AddOrUpdateApiScalarTypeConfiguration(ApiTypeConfiguration apiTypeConfiguration,
                                                          ApiPrecedenceLevel   apiPrecedenceLevel,
                                                          ApiPrecedenceStack   apiPrecedenceStack)
        {
            Contract.Requires(apiTypeConfiguration != null);
            Contract.Requires(apiPrecedenceStack != null);

            AddOrUpdateApiTypeConfiguration<ApiScalarTypeConfiguration, ApiMutableFactory<ApiMutableSchema, ApiMutableScalarType>, ApiMutableScalarType>(this.ApiScalarTypeConfigurationDictionary, apiTypeConfiguration,
                                                                                                                                                         ApiScalarTypeConfigurationOpenGenericType, apiPrecedenceLevel, apiPrecedenceStack);
        }

        public ApiEnumerationTypeConfiguration GetOrAddApiEnumerationTypeConfiguration(Type               clrEnumType,
                                                                                       ApiPrecedenceLevel apiPrecedenceLevel,
                                                                                       ApiPrecedenceStack apiPrecedenceStack)
        {
            Contract.Requires(clrEnumType != null);
            Contract.Requires(apiPrecedenceStack != null);

            var apiEnumTypeConfiguration = GetOrAddApiConfiguration<ApiEnumerationTypeConfiguration>(this.ApiEnumerationTypeConfigurationDictionary, clrEnumType, ApiEnumerationTypeConfigurationOpenGenericType, apiPrecedenceLevel, apiPrecedenceStack);
            return apiEnumTypeConfiguration;
        }

        public ApiEnumerationTypeConfiguration<TEnum> GetOrAddApiEnumerationTypeConfiguration<TEnum>(ApiPrecedenceLevel apiPrecedenceLevel,
                                                                                                     ApiPrecedenceStack apiPrecedenceStack)
            where TEnum : Enum
        {
            Contract.Requires(apiPrecedenceStack != null);

            var apiEnumTypeConfiguration = GetOrAddApiConfiguration<TEnum, ApiEnumerationTypeConfiguration<TEnum>>(this.ApiEnumerationTypeConfigurationDictionary, ApiEnumerationTypeConfigurationOpenGenericType, apiPrecedenceLevel, apiPrecedenceStack);
            return apiEnumTypeConfiguration;
        }

        public ApiObjectTypeConfiguration GetOrAddApiObjectTypeConfiguration(Type               clrObjectType,
                                                                             ApiPrecedenceLevel apiPrecedenceLevel,
                                                                             ApiPrecedenceStack apiPrecedenceStack)
        {
            Contract.Requires(clrObjectType != null);
            Contract.Requires(apiPrecedenceStack != null);

            var apiObjectTypeConfiguration = GetOrAddApiConfiguration<ApiObjectTypeConfiguration>(this.ApiObjectTypeConfigurationDictionary, clrObjectType, ApiObjectTypeConfigurationOpenGenericType, apiPrecedenceLevel, apiPrecedenceStack);
            return apiObjectTypeConfiguration;
        }

        public ApiObjectTypeConfiguration<TObject> GetOrAddApiObjectTypeConfiguration<TObject>(ApiPrecedenceLevel apiPrecedenceLevel,
                                                                                               ApiPrecedenceStack apiPrecedenceStack)
        {
            Contract.Requires(apiPrecedenceStack != null);

            var apiObjectTypeConfiguration = GetOrAddApiConfiguration<TObject, ApiObjectTypeConfiguration<TObject>>(this.ApiObjectTypeConfigurationDictionary, ApiObjectTypeConfigurationOpenGenericType, apiPrecedenceLevel, apiPrecedenceStack);
            return apiObjectTypeConfiguration;
        }

        public ApiScalarTypeConfiguration GetOrAddApiScalarTypeConfiguration(Type               clrScalarType,
                                                                             ApiPrecedenceLevel apiPrecedenceLevel,
                                                                             ApiPrecedenceStack apiPrecedenceStack)
        {
            Contract.Requires(clrScalarType != null);
            Contract.Requires(apiPrecedenceStack != null);

            var apiScalarTypeConfiguration = GetOrAddApiConfiguration<ApiScalarTypeConfiguration>(this.ApiScalarTypeConfigurationDictionary, clrScalarType, ApiScalarTypeConfigurationOpenGenericType, apiPrecedenceLevel, apiPrecedenceStack);
            return apiScalarTypeConfiguration;
        }

        public ApiScalarTypeConfiguration<TScalar> GetOrAddApiScalarTypeConfiguration<TScalar>(ApiPrecedenceLevel apiPrecedenceLevel,
                                                                                               ApiPrecedenceStack apiPrecedenceStack)
        {
            Contract.Requires(apiPrecedenceStack != null);

            var apiScalarTypeConfiguration = GetOrAddApiConfiguration<TScalar, ApiScalarTypeConfiguration<TScalar>>(this.ApiScalarTypeConfigurationDictionary, ApiScalarTypeConfigurationOpenGenericType, apiPrecedenceLevel, apiPrecedenceStack);
            return apiScalarTypeConfiguration;
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static void AddOrUpdateApiTypeConfiguration<TConfiguration, TMutableFactory, TMutable>(IDictionary<Type, ApiTypeConfiguration> dictionary,
                                                                                                       ApiTypeConfiguration                    apiTypeConfiguration,
                                                                                                       Type                                    apiTypeConfigurationOpenGenericType,
                                                                                                       ApiPrecedenceLevel                      apiPrecedenceLevel,
                                                                                                       ApiPrecedenceStack                      apiPrecedenceStack)
            where TConfiguration : ApiTypeConfiguration
            where TMutableFactory : ApiMutableFactory<ApiMutableSchema, TMutable>
        {
            Contract.Requires(dictionary != null);
            Contract.Requires(apiTypeConfiguration != null);
            Contract.Requires(apiTypeConfigurationOpenGenericType != null);
            Contract.Requires(apiPrecedenceStack != null);

            var apiConfigurationIncoming = (TConfiguration)apiTypeConfiguration;
            var apiObjectFactoryIncoming = (TMutableFactory)apiConfigurationIncoming.ApiMutableFactory;

            var clrType = apiTypeConfiguration.ClrType;
            if (dictionary.TryGetValue(clrType, out var apiConfiguration))
            {
                // Update the existing API configuration by merging the incoming derived API configuration.
                var apiConfigurationExisting = (TConfiguration)apiConfiguration;
                var apiObjectFactoryExisting = (TMutableFactory)apiConfigurationExisting.ApiMutableFactory;
                apiObjectFactoryExisting.Merge(apiObjectFactoryIncoming);
                return;
            }

            // Create a new API configuration from the incoming derived API configuration.
            var apiConfigurationNew = ApiTypeConfigurationFactory.CreateApiTypeConfiguration<TConfiguration, TMutableFactory, TMutable>(clrType, apiTypeConfigurationOpenGenericType, apiPrecedenceStack, apiObjectFactoryIncoming);
            dictionary.Add(clrType, apiConfigurationNew);

            ApiFrameworkLog.Trace($"Added {typeof(TConfiguration).Name} [ClrType={apiConfigurationNew.ClrType.Name}] at {apiPrecedenceLevel} Level".Indent(IndentConstants.ApiMutableSchemaModifier));
        }

        private static TApiConfiguration GetOrAddApiConfiguration<TApiConfiguration>(IDictionary<Type, ApiTypeConfiguration> dictionary,
                                                                                     Type                                    clrType,
                                                                                     Type                                    apiConfigurationOpenGenericType,
                                                                                     ApiPrecedenceLevel                      apiPrecedenceLevel,
                                                                                     ApiPrecedenceStack                      apiPrecedenceStack)
            where TApiConfiguration : ApiTypeConfiguration
        {
            Contract.Requires(dictionary != null);
            Contract.Requires(clrType != null);
            Contract.Requires(apiConfigurationOpenGenericType != null);
            Contract.Requires(apiPrecedenceStack != null);

            if (dictionary.TryGetValue(clrType, out var apiConfiguration))
            {
                var apiConfigurationExisting = (TApiConfiguration)apiConfiguration;
                return apiConfigurationExisting;
            }

            var apiConfigurationNew = ApiTypeConfigurationFactory.CreateApiTypeConfiguration<TApiConfiguration>(clrType, apiConfigurationOpenGenericType, apiPrecedenceStack);
            dictionary.Add(clrType, apiConfigurationNew);

            var apiTypeKind = apiConfigurationNew.ApiTypeKind;
            switch (apiTypeKind)
            {
                case ApiTypeKind.Enumeration:
                {
                    ApiFrameworkLog.Trace($"Added {nameof(ApiEnumerationType)} [{nameof(ApiEnumerationType.ClrType)}={clrType.Name}] at {apiPrecedenceLevel} Level".Indent(IndentConstants.ApiMutableSchemaModifier));
                    break;
                }

                case ApiTypeKind.Object:
                {
                    ApiFrameworkLog.Trace($"Added {nameof(ApiObjectType)} [{nameof(ApiObjectType.ClrType)}={clrType.Name}] at {apiPrecedenceLevel} Level".Indent(IndentConstants.ApiMutableSchemaModifier));
                    break;
                }

                case ApiTypeKind.Scalar:
                {
                    ApiFrameworkLog.Trace($"Added {nameof(ApiScalarType)} [{nameof(ApiScalarType.ClrType)}={clrType.Name}] at {apiPrecedenceLevel} Level".Indent(IndentConstants.ApiMutableSchemaModifier));
                    break;
                }

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return apiConfigurationNew;
        }

        private static TApiConfiguration GetOrAddApiConfiguration<T, TApiConfiguration>(IDictionary<Type, ApiTypeConfiguration> dictionary,
                                                                                        Type                                    apiConfigurationOpenGenericType,
                                                                                        ApiPrecedenceLevel                      apiPrecedenceLevel,
                                                                                        ApiPrecedenceStack                      apiPrecedenceStack)
            where TApiConfiguration : ApiTypeConfiguration
        {
            Contract.Requires(dictionary != null);
            Contract.Requires(apiConfigurationOpenGenericType != null);
            Contract.Requires(apiPrecedenceStack != null);

            var clrType = typeof(T);
            return GetOrAddApiConfiguration<TApiConfiguration>(dictionary, clrType, apiConfigurationOpenGenericType, apiPrecedenceLevel, apiPrecedenceStack);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private static readonly Type ApiEnumerationTypeConfigurationOpenGenericType = typeof(ApiEnumerationTypeConfiguration<>);
        private static readonly Type ApiObjectTypeConfigurationOpenGenericType      = typeof(ApiObjectTypeConfiguration<>);
        private static readonly Type ApiScalarTypeConfigurationOpenGenericType      = typeof(ApiScalarTypeConfiguration<>);
        #endregion
    }
}