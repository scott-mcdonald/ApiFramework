// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;

using ApiFramework.Exceptions;
using ApiFramework.Reflection;
using ApiFramework.Schema.Configuration.Internal;
using ApiFramework.Schema.Conventions;
using ApiFramework.Schema.Internal;

namespace ApiFramework.Schema.Configuration
{
    /// <inheritdoc cref="IApiObjectTypeBuilder"/>
    /// <summary>
    /// Represents the configuration to build and create an API object type in an API schema.
    /// </summary>
    public abstract class ApiObjectTypeConfiguration : ApiTypeConfiguration, IApiObjectTypeBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiObjectTypeBuilder Implementation
        IApiObjectTypeBuilder IApiObjectTypeBuilder.Exclude(string clrPropertyName)
        {
            Contract.Requires(clrPropertyName.SafeHasContent());

            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutableObjectTypeFactory.AddModifier(apiConfigurationLevel,
                                                         x =>
                                                         {
                                                             ApiFrameworkLog.Trace($"Excluded {nameof(ApiProperty)} [{nameof(ApiProperty.ClrName)}={clrPropertyName}] at {apiConfigurationLevel} Level".Indent(IndentConstants.ApiMutableObjectTypeModifier));
                                                             x.ClrExcludedPropertyNames.Add(clrPropertyName);
                                                         },
                                                         CallerTypeName);
            return this;
        }

        IApiObjectTypeBuilder IApiObjectTypeBuilder.HasName(string apiName)
        {
            Contract.Requires(apiName.SafeHasContent());

            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutableObjectTypeFactory.AddModifier(apiConfigurationLevel,
                                                         x =>
                                                         {
                                                             ApiFrameworkLog.Trace($"Modified {nameof(x.ApiName)} '{x.ApiName}' => '{apiName}' at {apiConfigurationLevel} Level".Indent(IndentConstants.ApiMutableObjectTypeModifier));
                                                             x.ApiName = apiName;
                                                         },
                                                         CallerTypeName);
            return this;
        }

        IApiObjectTypeBuilder IApiObjectTypeBuilder.HasDescription(string apiDescription)
        {
            Contract.Requires(apiDescription.SafeHasContent());

            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutableObjectTypeFactory.AddModifier(apiConfigurationLevel,
                                                         x =>
                                                         {
                                                             ApiFrameworkLog.Trace($"Modified {nameof(x.ApiDescription)} '{x.ApiDescription}' => '{apiDescription}' at {apiConfigurationLevel} Level".Indent(IndentConstants.ApiMutableObjectTypeModifier));
                                                             x.ApiDescription = apiDescription;
                                                         },
                                                         CallerTypeName);
            return this;
        }

        IApiObjectTypeBuilder IApiObjectTypeBuilder.ApiProperty(string clrPropertyName, Type clrPropertyType, Func<IApiPropertyBuilder, IApiPropertyBuilder> configuration)
        {
            Contract.Requires(clrPropertyName.SafeHasContent());
            Contract.Requires(clrPropertyType != null);

            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            var clrDeclaringType      = this.ClrType;
            this.ApiMutableObjectTypeFactory.AddModifier(apiConfigurationLevel,
                                                         x =>
                                                         {
                                                             var apiPrecedenceStack       = this.ApiPrecedenceStack;
                                                             var apiPropertyConfiguration = GetOrAddApiPropertyConfiguration(x, clrDeclaringType, clrPropertyName, clrPropertyType, apiConfigurationLevel, apiPrecedenceStack);
                                                             configuration?.Invoke(apiPropertyConfiguration);
                                                         },
                                                         CallerTypeName);
            return this;
        }

        IApiObjectTypeBuilder IApiObjectTypeBuilder.ApiIdentity(string clrPropertyName, Type clrPropertyType)
        {
            Contract.Requires(clrPropertyName.SafeHasContent());
            Contract.Requires(clrPropertyType != null);

            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutableObjectTypeFactory.AddModifier(apiConfigurationLevel,
                                                         x =>
                                                         {
                                                             // This API object type is an API resource type because it has identity, register with the mutable API schema object.
                                                             var clrObjectType    = x.ClrObjectType;
                                                             var apiMutableSchema = x.ApiMutableSchema;
                                                             apiMutableSchema.AddClrResourceType(clrObjectType);

                                                             var clrIdentityProperty = new ClrIdentityProperty(clrPropertyName, clrPropertyType);
                                                             ApiFrameworkLog.Trace($"Set {clrIdentityProperty} at {apiConfigurationLevel} Level".Indent(IndentConstants.ApiMutableObjectTypeModifier));

                                                             x.ClrIdentityProperty = clrIdentityProperty;
                                                         },
                                                         CallerTypeName);
            return this;
        }

        IApiObjectTypeBuilder IApiObjectTypeBuilder.ApiRelationship(string clrPropertyName, Type clrPropertyType)
        {
            Contract.Requires(clrPropertyName.SafeHasContent());
            Contract.Requires(clrPropertyType != null);

            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutableObjectTypeFactory.AddModifier(apiConfigurationLevel,
                                                         x =>
                                                         {
                                                             // Preserve the order in which API relationships are added.
                                                             if (x.ClrRelationshipPropertyIndexDictionary.TryGetValue(clrPropertyName, out var clrRelationshipPropertyIndex))
                                                                 return;

                                                             var clrRelationshipProperty = new ClrRelationshipProperty(clrPropertyName, clrPropertyType);
                                                             clrRelationshipPropertyIndex = x.ClrRelationshipPropertyIndexDictionary.Count;

                                                             ApiFrameworkLog.Trace($"Added {clrRelationshipProperty} at {apiConfigurationLevel} Level".Indent(IndentConstants.ApiMutableObjectTypeModifier));

                                                             x.ClrRelationshipPropertyCollection.Add(clrRelationshipProperty);
                                                             x.ClrRelationshipPropertyIndexDictionary.Add(clrPropertyName, clrRelationshipPropertyIndex);
                                                         },
                                                         CallerTypeName);
            return this;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ApiObjectTypeConfiguration(Type clrObjectType, ApiPrecedenceStack apiPrecedenceStack)
            : base(clrObjectType)
        {
            Contract.Requires(clrObjectType != null);
            Contract.Requires(apiPrecedenceStack != null);

            this.ApiPrecedenceStack          = apiPrecedenceStack;
            this.ApiMutableObjectTypeFactory = new ApiMutableFactory<ApiMutableSchema, ApiMutableObjectType>(this.CreateApiMutableObjectTypeFactory(clrObjectType), IndentConstants.ApiMutableObjectType);
        }

        internal ApiObjectTypeConfiguration(Type clrObjectType, ApiPrecedenceStack apiPrecedenceStack, ApiMutableFactory<ApiMutableSchema, ApiMutableObjectType> apiMutableObjectTypeFactory)
            : base(clrObjectType)
        {
            Contract.Requires(clrObjectType != null);
            Contract.Requires(apiPrecedenceStack != null);

            this.ApiPrecedenceStack          = apiPrecedenceStack;
            this.ApiMutableObjectTypeFactory = apiMutableObjectTypeFactory;
        }
        #endregion

        // INTERNAL PROPERTIES //////////////////////////////////////////////
        #region ApiTypeConfiguration Overrides
        internal override ApiTypeKind ApiTypeKind => ApiTypeKind.Object;

        internal override ApiMutableFactory ApiMutableFactory => this.ApiMutableObjectTypeFactory;
        #endregion

        #region Properties
        internal ApiPrecedenceStack ApiPrecedenceStack { get; }
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region ApiTypeConfiguration Overrides
        internal override ApiMutableType CreateApiMutableType(ApiMutableSchema apiMutableSchema)
        {
            Contract.Requires(apiMutableSchema != null);

            var apiMutableObjectType = this.ApiMutableObjectTypeFactory.Create(apiMutableSchema);
            return apiMutableObjectType;
        }

        internal override IApiType CreateApiType(ApiMutableType apiMutableType, ApiSchemaProxy apiSchemaProxy)
        {
            Contract.Requires(apiMutableType != null);
            Contract.Requires(apiSchemaProxy != null);

            var apiMutableObjectType = (ApiMutableObjectType)apiMutableType;

            ApiFrameworkLog.Debug($"Creating {nameof(ApiObjectType)}".Indent(IndentConstants.ApiObjectType));
            var apiObjectType = CreateApiObjectType(apiMutableObjectType, apiSchemaProxy);
            ApiFrameworkLog.Debug($"Created {apiObjectType}".Indent(IndentConstants.ApiObjectType));

            return apiObjectType;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private ApiMutableFactory<ApiMutableSchema, ApiMutableObjectType> ApiMutableObjectTypeFactory { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void ApplyApiObjectTypeNameConventions(Type clrObjectType, IApiConventionSet apiConventionSet, ApiConventionSettings apiConventionSettings)
        {
            Contract.Requires(clrObjectType != null);
            Contract.Requires(apiConventionSet != null);

            var apiObjectTypeNameConventions = apiConventionSet?.ApiObjectTypeNameConventions;
            if (apiObjectTypeNameConventions == null)
                return;

            var apiObjectTypeNameConventionsCollection = apiObjectTypeNameConventions.SafeToReadOnlyCollection();
            if (!apiObjectTypeNameConventionsCollection.Any())
                return;

            var apiName = clrObjectType.Name;
            apiName = apiObjectTypeNameConventionsCollection.Aggregate(apiName, (current, apiNamingConvention) => apiNamingConvention.Apply(current, apiConventionSettings));

            var apiObjectTypeBuilder = (IApiObjectTypeBuilder)this;
            apiObjectTypeBuilder.HasName(apiName);
        }

        private void ApplyApiObjectTypeConventions(IApiConventionSet apiConventionSet, ApiConventionSettings apiConventionSettings)
        {
            Contract.Requires(apiConventionSet != null);

            var apiObjectTypeConventions = apiConventionSet?.ApiObjectTypeConventions;
            if (apiObjectTypeConventions == null)
                return;

            foreach (var apiObjectTypeConvention in apiObjectTypeConventions)
            {
                apiObjectTypeConvention.Apply(this, apiConventionSettings);
            }
        }

        private Func<ApiMutableSchema, ApiMutableObjectType> CreateApiMutableObjectTypeFactory(Type clrObjectType)
        {
            Contract.Requires(clrObjectType != null);

            ApiMutableObjectType ApiMutableObjectTypeFactory(ApiMutableSchema apiMutableSchema)
            {
                Contract.Requires(apiMutableSchema != null);

                // Apply conventions
                this.ApiPrecedenceStack.Push(ApiPrecedenceLevel.Convention);

                var apiConventionSet      = apiMutableSchema?.ApiConventionSet;
                var apiConventionSettings = apiMutableSchema?.ApiConventionSettings;
                this.ApplyApiObjectTypeNameConventions(clrObjectType, apiConventionSet, apiConventionSettings);
                this.ApplyApiObjectTypeConventions(apiConventionSet, apiConventionSettings);

                this.ApiPrecedenceStack.Pop();

                // Create API mutable object type
                var apiDefaultName        = clrObjectType.Name;
                var apiDefaultDescription = clrObjectType.ToString();
                var apiMutableObjectType = new ApiMutableObjectType
                {
                    ApiMutableSchema = apiMutableSchema,
                    ApiName          = apiDefaultName,
                    ApiDescription   = apiDefaultDescription,
                    ClrObjectType    = clrObjectType
                };
                return apiMutableObjectType;
            }

            return ApiMutableObjectTypeFactory;
        }

        private static IApiObjectType CreateApiObjectType(ApiMutableObjectType apiMutableObjectType, ApiSchemaProxy apiSchemaProxy)
        {
            Contract.Requires(apiMutableObjectType != null);
            Contract.Requires(apiSchemaProxy != null);

            var apiName          = apiMutableObjectType.ApiName;
            var apiDescription   = apiMutableObjectType.ApiDescription;
            var apiProperties    = CreateApiProperties(apiMutableObjectType, apiSchemaProxy);
            var apiIdentity      = CreateApiIdentity(apiMutableObjectType, apiProperties);
            var apiRelationships = CreateApiRelationships(apiMutableObjectType, apiSchemaProxy, apiProperties, apiIdentity);
            var clrObjectType    = apiMutableObjectType.ClrObjectType;
            var apiObjectType    = ApiTypeFactory.CreateApiObjectType(apiName, apiDescription, apiProperties, apiIdentity, apiRelationships, clrObjectType);
            return apiObjectType;
        }

        private static IApiIdentity CreateApiIdentity(ApiMutableObjectType apiMutableObjectType, IEnumerable<IApiProperty> apiProperties)
        {
            Contract.Requires(apiMutableObjectType != null);
            Contract.Requires(apiProperties != null);

            var clrIdentityProperty = apiMutableObjectType.ClrIdentityProperty;
            if (clrIdentityProperty == null)
                return null;

            var clrIdentityPropertyName = clrIdentityProperty.ClrPropertyName;
            if (String.IsNullOrWhiteSpace(clrIdentityPropertyName))
                return null;

            var apiIdentityProperty = (ApiProperty)apiProperties.SingleOrDefault(x => x.ClrName == clrIdentityPropertyName);
            if (apiIdentityProperty == null)
            {
                var clrTypeName = apiMutableObjectType.ClrObjectType.Name;
                var message     = $"Unable to create API identity for an API object type [clrType={clrTypeName}] because the configured API identity property [clrName={clrIdentityPropertyName}] does not exist.";
                throw new ApiSchemaException(message);
            }

            var apiIdentity = ApiTypeFactory.CreateApiIdentity(apiIdentityProperty);

            ApiFrameworkLog.Debug($"Created {apiIdentity}".Indent(IndentConstants.ApiMutableObjectTypeIdentity));

            return apiIdentity;
        }

        private static IReadOnlyCollection<IApiProperty> CreateApiProperties(ApiMutableObjectType apiMutableObjectType, ApiSchemaProxy apiSchemaProxy)
        {
            Contract.Requires(apiMutableObjectType != null);
            Contract.Requires(apiSchemaProxy != null);

            // Create an ApiPropertyConfiguration query (LINQ to Objects).
            var apiMutableSchema               = apiMutableObjectType.ApiMutableSchema;
            var apiPropertyConfigurations      = apiMutableObjectType.ApiPropertyConfigurations;
            var apiPropertyConfigurationsQuery = apiPropertyConfigurations.AsEnumerable();

            // Enhance query to remove explicit (by name) excluded properties for this API object type.
            var clrExplicitExcludedPropertyNames = apiMutableObjectType.ClrExcludedPropertyNames;
            if (clrExplicitExcludedPropertyNames.Any())
            {
                var clrExplicitIncludedPropertyNames = apiPropertyConfigurations.Select(x => x.ClrName)
                                                                                .ToList();
                var clrExplicitIncludedPropertyNameHashSet = new HashSet<string>(clrExplicitIncludedPropertyNames);
                clrExplicitIncludedPropertyNameHashSet.ExceptWith(clrExplicitExcludedPropertyNames);

                apiPropertyConfigurationsQuery = apiPropertyConfigurationsQuery.Where(x => clrExplicitIncludedPropertyNameHashSet.Contains(x.ClrName));
            }

            // Enhance query to remove implicit (by type) excluded properties for this API object type.
            var clrExcludedTypes = apiMutableSchema.ClrExcludedTypes;
            if (clrExcludedTypes.Any())
            {
                var clrImplicitExcludedPropertyNameHashSet =
                    apiPropertyConfigurations
                        .Where(x =>
                        {
                            var clrLeafType = x.ClrType.GetClrLeafType();
                            return clrExcludedTypes.Contains(clrLeafType);
                        })
                        .Select(x => x.ClrName)
                        .ToList();

                var clrImplicitIncludedPropertyNames = apiPropertyConfigurations.Select(x => x.ClrName)
                                                                                .ToList();
                var clrImplicitIncludedPropertyNameHashSet = new HashSet<string>(clrImplicitIncludedPropertyNames);
                clrImplicitIncludedPropertyNameHashSet.ExceptWith(clrImplicitExcludedPropertyNameHashSet);

                apiPropertyConfigurationsQuery = apiPropertyConfigurationsQuery.Where(x => clrImplicitIncludedPropertyNameHashSet.Contains(x.ClrName));
            }

            // Execute query to create API property objects for this API object type.
            var apiProperties = apiPropertyConfigurationsQuery.Select(x => x.CreateApiProperty(apiMutableSchema,
                                                                                               apiMutableObjectType,
                                                                                               apiSchemaProxy))
                                                              .ToList();
            return apiProperties;
        }

        // ReSharper disable once ReturnTypeCanBeEnumerable.Local
        private static IReadOnlyCollection<IApiRelationship> CreateApiRelationships(ApiMutableObjectType      apiMutableObjectType,
                                                                                    ApiSchemaProxy            apiSchemaProxy,
                                                                                    IEnumerable<IApiProperty> apiProperties,
                                                                                    IApiIdentity              apiIdentity)
        {
            Contract.Requires(apiMutableObjectType != null);
            Contract.Requires(apiSchemaProxy != null);
            Contract.Requires(apiProperties != null);

            // If this API object type has no identity, it may not contain any relationships.
            if (apiIdentity == null)
                return null;

            var apiMutableSchema                  = apiMutableObjectType.ApiMutableSchema;
            var clrRelationshipPropertyCollection = apiMutableObjectType.ClrRelationshipPropertyCollection;
            var apiRelationships = clrRelationshipPropertyCollection
                                   .Where(x =>
                                   {
                                       // Can not create an API relationship if the related API object type does not have identity (must be an API resource type).
                                       var clrPropertyType     = x.ClrPropertyType;
                                       var apiPropertyTypeKind = clrPropertyType.GetApiTypeKind(out var clrPropertyItemType);
                                       switch (apiPropertyTypeKind)
                                       {
                                           case ApiTypeKind.Object:
                                           {
                                               var isApiResourceType = apiMutableSchema.ClrResourceTypes.Contains(clrPropertyType);
                                               if (!isApiResourceType)
                                               {
                                                   return false;
                                               }

                                               break;
                                           }

                                           case ApiTypeKind.Collection:
                                           {
                                               var apiItemTypeKind = clrPropertyItemType.GetApiTypeKind();
                                               switch (apiItemTypeKind)
                                               {
                                                   case ApiTypeKind.Collection:
                                                   {
                                                       // Unable to handle collections within collections.
                                                       var message = $"Unable to create API relationship for an API property [{nameof(x.ClrPropertyName)}={x.ClrPropertyName}] that contains collections within collections";
                                                       throw new ApiSchemaException(message);
                                                   }
                                               }

                                               var isApiResourceType = apiMutableSchema.ClrResourceTypes.Contains(clrPropertyItemType);
                                               if (!isApiResourceType)
                                               {
                                                   return false;
                                               }

                                               break;
                                           }

                                           default:
                                           {
                                               return false;
                                           }
                                       }

                                       return true;
                                   })
                                   .Select(x =>
                                   {
                                       var clrPropertyName     = x.ClrPropertyName;
                                       var clrPropertyType     = x.ClrPropertyType;
                                       var apiProperty         = apiProperties.Single(y => y.ClrName == clrPropertyName);
                                       var apiPropertyTypeKind = clrPropertyType.GetApiTypeKind(out var clrPropertyItemType);

                                       ApiRelationshipCardinality apiCardinality;
                                       Type                       clrType;
                                       switch (apiPropertyTypeKind)
                                       {
                                           case ApiTypeKind.Object:
                                           {
                                               apiCardinality = ApiRelationshipCardinality.ToOne;
                                               clrType        = clrPropertyType;
                                               break;
                                           }

                                           case ApiTypeKind.Collection:
                                           {
                                               apiCardinality = ApiRelationshipCardinality.ToMany;
                                               clrType        = clrPropertyItemType;
                                               break;
                                           }

                                           default:
                                           {
                                               throw new ArgumentOutOfRangeException();
                                           }
                                       }

                                       var apiRelatedTypeResolver = new ApiSchemaProxyTypeResolver(apiSchemaProxy, ApiTypeKind.Object, clrType);
                                       var apiRelationship        = ApiTypeFactory.CreateApiRelationship(apiProperty, apiCardinality, apiRelatedTypeResolver);

                                       return apiRelationship;
                                   })
                                   .ToList();

            foreach (var apiRelationship in apiRelationships)
            {
                ApiFrameworkLog.Debug($"Created {apiRelationship}".Indent(IndentConstants.ApiMutableObjectTypeRelationship));
            }

            return apiRelationships;
        }

        private static ApiPropertyConfiguration GetOrAddApiPropertyConfiguration(ApiMutableObjectType apiMutableObjectType,
                                                                                 Type                 clrDeclaringType,
                                                                                 string               clrPropertyName,
                                                                                 Type                 clrPropertyType,
                                                                                 ApiPrecedenceLevel   apiPrecedenceLevel,
                                                                                 ApiPrecedenceStack   apiPrecedenceStack)
        {
            Contract.Requires(apiMutableObjectType != null);
            Contract.Requires(clrDeclaringType != null);
            Contract.Requires(clrPropertyName.SafeHasContent());
            Contract.Requires(clrPropertyType != null);
            Contract.Requires(apiPrecedenceStack != null);

            if (apiMutableObjectType.ApiPropertyConfigurationsIndex.TryGetValue(clrPropertyName, out var apiPropertyConfigurationIndex))
            {
                var apiPropertyConfigurationExisting = apiMutableObjectType.ApiPropertyConfigurations[apiPropertyConfigurationIndex];
                return apiPropertyConfigurationExisting;
            }

            var apiPropertyConfigurationNew      = new ApiPropertyConfiguration(clrDeclaringType, clrPropertyName, clrPropertyType, apiPrecedenceStack);
            var apiPropertyConfigurationIndexNew = apiMutableObjectType.ApiPropertyConfigurationsIndex.Count;

            apiMutableObjectType.ApiPropertyConfigurations.Add(apiPropertyConfigurationNew);
            apiMutableObjectType.ApiPropertyConfigurationsIndex.Add(clrPropertyName, apiPropertyConfigurationIndexNew);

            ApiFrameworkLog.Trace($"Added {nameof(ApiProperty)} [{nameof(ApiProperty.ClrName)}={clrPropertyName}] at {apiPrecedenceLevel} Level".Indent(IndentConstants.ApiMutableObjectTypeModifier));

            return apiPropertyConfigurationNew;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private const string CallerTypeName = nameof(ApiObjectTypeConfiguration);
        #endregion
    }

    /// <inheritdoc cref="IApiObjectTypeBuilder{TObject}"/>
    /// <inheritdoc cref="ApiObjectTypeConfiguration"/>
    /// <typeparam name="TObject">
    /// The CLR object type associated to the API object type for CLR serializing/deserializing purposes.
    /// </typeparam>
    public class ApiObjectTypeConfiguration<TObject> : ApiObjectTypeConfiguration, IApiObjectTypeBuilder<TObject>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiObjectTypeConfiguration()
            : base(typeof(TObject), new ApiPrecedenceStack(ApiPrecedenceLevel.TypeConfiguration))
        {
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiObjectTypeBuilder<TObject> Implementation
        public IApiObjectTypeBuilder<TObject> Exclude<TProperty>(Expression<Func<TObject, TProperty>> clrPropertySelector)
        {
            Contract.Requires(clrPropertySelector != null);

            var clrPropertyName      = StaticReflection.GetMemberName(clrPropertySelector);
            var apiObjectTypeBuilder = (IApiObjectTypeBuilder)this;
            apiObjectTypeBuilder.Exclude(clrPropertyName);
            return this;
        }

        public IApiObjectTypeBuilder<TObject> HasName(string apiName)
        {
            Contract.Requires(apiName.SafeHasContent());

            var apiObjectTypeBuilder = (IApiObjectTypeBuilder)this;
            apiObjectTypeBuilder.HasName(apiName);
            return this;
        }

        public IApiObjectTypeBuilder<TObject> HasDescription(string apiDescription)
        {
            Contract.Requires(apiDescription.SafeHasContent());

            var apiObjectTypeBuilder = (IApiObjectTypeBuilder)this;
            apiObjectTypeBuilder.HasDescription(apiDescription);
            return this;
        }

        public IApiObjectTypeBuilder<TObject> ApiProperty<TProperty>(Expression<Func<TObject, TProperty>> clrPropertySelector, Func<IApiPropertyBuilder, IApiPropertyBuilder> configuration)
        {
            Contract.Requires(clrPropertySelector != null);

            var clrPropertyName      = StaticReflection.GetMemberName(clrPropertySelector);
            var clrPropertyType      = typeof(TProperty);
            var apiObjectTypeBuilder = (IApiObjectTypeBuilder)this;
            apiObjectTypeBuilder.ApiProperty(clrPropertyName, clrPropertyType, configuration);
            return this;
        }

        public IApiObjectTypeBuilder<TObject> ApiIdentity<TProperty>(Expression<Func<TObject, TProperty>> clrPropertySelector)
        {
            Contract.Requires(clrPropertySelector != null);

            var clrPropertyName      = StaticReflection.GetMemberName(clrPropertySelector);
            var clrPropertyType      = typeof(TProperty);
            var apiObjectTypeBuilder = (IApiObjectTypeBuilder)this;
            apiObjectTypeBuilder.ApiIdentity(clrPropertyName, clrPropertyType);
            return this;
        }

        public IApiObjectTypeBuilder<TObject> ApiRelationship<TProperty>(Expression<Func<TObject, TProperty>> clrPropertySelector)
        {
            Contract.Requires(clrPropertySelector != null);

            var clrPropertyName      = StaticReflection.GetMemberName(clrPropertySelector);
            var clrPropertyType      = typeof(TProperty);
            var apiObjectTypeBuilder = (IApiObjectTypeBuilder)this;
            apiObjectTypeBuilder.ApiRelationship(clrPropertyName, clrPropertyType);
            return this;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ApiObjectTypeConfiguration(ApiPrecedenceStack apiPrecedenceStack)
            : base(typeof(TObject), apiPrecedenceStack)
        {
        }

        internal ApiObjectTypeConfiguration(ApiPrecedenceStack apiPrecedenceStack, ApiMutableFactory<ApiMutableSchema, ApiMutableObjectType> apiMutableObjectTypeFactory)
            : base(typeof(TObject), apiPrecedenceStack, apiMutableObjectTypeFactory)
        {
        }
        #endregion
    }
}