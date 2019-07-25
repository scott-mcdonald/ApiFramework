// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using ApiFramework.Schema.Configuration.Internal;
using ApiFramework.Schema.Internal;

namespace ApiFramework.Schema.Configuration
{
    /// <inheritdoc cref="IApiSchemaBuilder"/>
    /// <inheritdoc cref="IApiSchemaFactory"/>
    /// <summary>
    /// Represents the configuration to build and create an API schema.
    /// </summary>
    public class ApiSchemaConfiguration : IApiSchemaBuilder, IApiSchemaFactory
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        /// <summary>
        /// Creates an empty API schema configuration object.
        /// </summary>
        public ApiSchemaConfiguration()
        {
            this.ApiPrecedenceStack      = new ApiPrecedenceStack(ApiPrecedenceLevel.FluentApi);
            this.ApiMutableSchemaFactory = new ApiMutableFactory<ApiSchemaFactorySettings, ApiMutableSchema>(this.CreateApiMutableSchemaFactory(), IndentConstants.ApiMutableSchema);
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiSchemaBuilder Implementation
        public IApiSchemaBuilder Exclude(Type clrType)
        {
            Contract.Requires(clrType != null);

            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            var apiTypeKind           = clrType.GetApiTypeKind();
            switch (apiTypeKind)
            {
                case ApiTypeKind.Object:
                {
                    this.ApiMutableSchemaFactory.AddModifier(apiConfigurationLevel,
                                                             x =>
                                                             {
                                                                 ApiFrameworkLog.Trace(
                                                                     $"Excluded {nameof(Schema.Internal.ApiObjectType)} [{nameof(Schema.Internal.ApiObjectType.ClrType)}={clrType.Name}] at {apiConfigurationLevel} Level".Indent(
                                                                         IndentConstants.ApiMutableSchemaModifier));
                                                                 x.AddClrExcludedObjectType(clrType);
                                                             },
                                                             CallerTypeName);
                    break;
                }

                case ApiTypeKind.Scalar:
                {
                    this.ApiMutableSchemaFactory.AddModifier(apiConfigurationLevel,
                                                             x =>
                                                             {
                                                                 ApiFrameworkLog.Trace(
                                                                     $"Excluded {nameof(Schema.Internal.ApiScalarType)} [{nameof(Schema.Internal.ApiScalarType.ClrType)}={clrType.Name}] at {apiConfigurationLevel} Level".Indent(
                                                                         IndentConstants.ApiMutableSchemaModifier));
                                                                 x.AddClrExcludedScalarType(clrType);
                                                             },
                                                             CallerTypeName);
                    break;
                }

                case ApiTypeKind.Enumeration:
                {
                    this.ApiMutableSchemaFactory.AddModifier(apiConfigurationLevel,
                                                             x =>
                                                             {
                                                                 ApiFrameworkLog.Trace(
                                                                     $"Excluded {nameof(Schema.Internal.ApiEnumerationType)} [{nameof(Schema.Internal.ApiEnumerationType.ClrType)}={clrType.Name}] at {apiConfigurationLevel} Level".Indent(
                                                                         IndentConstants.ApiMutableSchemaModifier));
                                                                 x.AddClrExcludedEnumerationType(clrType);
                                                             },
                                                             CallerTypeName);
                    break;
                }
            }

            return this;
        }

        public IApiSchemaBuilder HasName(string apiName)
        {
            Contract.Requires(apiName.SafeHasContent());

            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutableSchemaFactory.AddModifier(apiConfigurationLevel,
                                                     x =>
                                                     {
                                                         ApiFrameworkLog.Trace($"Modified {nameof(x.ApiName)} '{x.ApiName}' => '{apiName}' at {apiConfigurationLevel} Level".Indent(IndentConstants.ApiMutableSchemaModifier));
                                                         x.ApiName = apiName;
                                                     },
                                                     CallerTypeName);
            return this;
        }

        public IApiSchemaBuilder HasConfiguration(ApiTypeConfiguration apiTypeConfiguration)
        {
            Contract.Requires(apiTypeConfiguration != null);

            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutableSchemaFactory.AddModifier(apiConfigurationLevel, x =>
                                                     {
                                                         var apiPrecedenceStack = this.ApiPrecedenceStack;
                                                         var apiTypeKind        = apiTypeConfiguration.ApiTypeKind;
                                                         switch (apiTypeKind)
                                                         {
                                                             case ApiTypeKind.Scalar:
                                                             {
                                                                 x.AddOrUpdateApiScalarTypeConfiguration(apiTypeConfiguration, apiConfigurationLevel, apiPrecedenceStack);
                                                                 break;
                                                             }

                                                             case ApiTypeKind.Object:
                                                             {
                                                                 x.AddOrUpdateApiObjectTypeConfiguration(apiTypeConfiguration, apiConfigurationLevel, apiPrecedenceStack);
                                                                 break;
                                                             }

                                                             case ApiTypeKind.Enumeration:
                                                             {
                                                                 x.AddOrUpdateApiEnumerationTypeConfiguration(apiTypeConfiguration, apiConfigurationLevel, apiPrecedenceStack);
                                                                 break;
                                                             }

                                                             default:
                                                             {
                                                                 throw new ArgumentOutOfRangeException();
                                                             }
                                                         }
                                                     },
                                                     CallerTypeName);
            return this;
        }

        public IApiSchemaBuilder ApiEnumerationType(Type clrEnumerationType, Func<IApiEnumerationTypeBuilder, IApiEnumerationTypeBuilder> configuration)
        {
            Contract.Requires(clrEnumerationType != null);

            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutableSchemaFactory.AddModifier(apiConfigurationLevel, x =>
                                                     {
                                                         var apiPrecedenceStack              = this.ApiPrecedenceStack;
                                                         var apiEnumerationTypeConfiguration = x.GetOrAddApiEnumerationTypeConfiguration(clrEnumerationType, apiConfigurationLevel, apiPrecedenceStack);
                                                         configuration?.Invoke(apiEnumerationTypeConfiguration);
                                                     },
                                                     CallerTypeName);
            return this;
        }

        public IApiSchemaBuilder ApiObjectType(Type clrObjectType, Func<IApiObjectTypeBuilder, IApiObjectTypeBuilder> configuration)
        {
            Contract.Requires(clrObjectType != null);

            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutableSchemaFactory.AddModifier(apiConfigurationLevel, x =>
                                                     {
                                                         var apiPrecedenceStack         = this.ApiPrecedenceStack;
                                                         var apiObjectTypeConfiguration = x.GetOrAddApiObjectTypeConfiguration(clrObjectType, apiConfigurationLevel, apiPrecedenceStack);
                                                         configuration?.Invoke(apiObjectTypeConfiguration);
                                                     },
                                                     CallerTypeName);
            return this;
        }

        public IApiSchemaBuilder ApiScalarType(Type clrScalarType, Func<IApiScalarTypeBuilder, IApiScalarTypeBuilder> configuration)
        {
            Contract.Requires(clrScalarType != null);

            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutableSchemaFactory.AddModifier(apiConfigurationLevel, x =>
                                                     {
                                                         var apiPrecedenceStack         = this.ApiPrecedenceStack;
                                                         var apiScalarTypeConfiguration = x.GetOrAddApiScalarTypeConfiguration(clrScalarType, apiConfigurationLevel, apiPrecedenceStack);
                                                         configuration?.Invoke(apiScalarTypeConfiguration);
                                                     },
                                                     CallerTypeName);
            return this;
        }

        public IApiSchemaBuilder ApiEnumerationType<TEnumeration>(Func<IApiEnumerationTypeBuilder<TEnumeration>, IApiEnumerationTypeBuilder<TEnumeration>> configuration)
            where TEnumeration : Enum
        {
            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutableSchemaFactory.AddModifier(apiConfigurationLevel, x =>
                                                     {
                                                         var apiPrecedenceStack              = this.ApiPrecedenceStack;
                                                         var apiEnumerationTypeConfiguration = x.GetOrAddApiEnumerationTypeConfiguration<TEnumeration>(apiConfigurationLevel, apiPrecedenceStack);
                                                         configuration?.Invoke(apiEnumerationTypeConfiguration);
                                                     },
                                                     CallerTypeName);
            return this;
        }

        public IApiSchemaBuilder ApiObjectType<TObject>(Func<IApiObjectTypeBuilder<TObject>, IApiObjectTypeBuilder<TObject>> configuration)
        {
            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutableSchemaFactory.AddModifier(apiConfigurationLevel, x =>
                                                     {
                                                         var apiPrecedenceStack         = this.ApiPrecedenceStack;
                                                         var apiObjectTypeConfiguration = x.GetOrAddApiObjectTypeConfiguration<TObject>(apiConfigurationLevel, apiPrecedenceStack);
                                                         configuration?.Invoke(apiObjectTypeConfiguration);
                                                     },
                                                     CallerTypeName);
            return this;
        }

        public IApiSchemaBuilder ApiScalarType<TScalar>(Func<IApiScalarTypeBuilder<TScalar>, IApiScalarTypeBuilder<TScalar>> configuration)
        {
            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutableSchemaFactory.AddModifier(apiConfigurationLevel, x =>
                                                     {
                                                         var apiPrecedenceStack         = this.ApiPrecedenceStack;
                                                         var apiScalarTypeConfiguration = x.GetOrAddApiScalarTypeConfiguration<TScalar>(apiConfigurationLevel, apiPrecedenceStack);
                                                         configuration?.Invoke(apiScalarTypeConfiguration);
                                                     },
                                                     CallerTypeName);
            return this;
        }
        #endregion

        #region IApiSchemaFactory Implementation
        public IApiSchema Create(ApiSchemaFactorySettings apiSchemaFactorySettings)
        {
            ApiFrameworkLog.Debug($"Creating {nameof(ApiSchema)}".Indent(IndentConstants.ApiSchema));

            var apiMutableSchema = this.ApiMutableSchemaFactory.Create(apiSchemaFactorySettings);

            var apiSchema = CreateApiSchema(apiMutableSchema, this.ApiPrecedenceStack);

            var createdApiSchema = apiSchema.ToTreeString();
            ApiFrameworkLog.Information($"Created {nameof(ApiSchema)}" + "\n" + "{CreatedApiSchema}".Indent(IndentConstants.ApiSchema), createdApiSchema);

            return apiSchema;
        }
        #endregion

        // INTERNAL PROPERTIES //////////////////////////////////////////////
        #region Properties
        internal ApiPrecedenceStack ApiPrecedenceStack { get; }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private ApiMutableFactory<ApiSchemaFactorySettings, ApiMutableSchema> ApiMutableSchemaFactory { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private static void AddMissingApiEnumerationTypes(ApiMutableSchema apiMutableSchema, ApiPrecedenceStack apiPrecedenceStack)
        {
            Contract.Requires(apiMutableSchema != null);
            Contract.Requires(apiPrecedenceStack != null);

            var clrExplicitEnumerationTypes = apiMutableSchema.ApiEnumerationTypeConfigurationDictionary
                                                              .Keys
                                                              .ToList();

            var clrMissingEnumerationTypes = new HashSet<Type>(apiMutableSchema.ClrImplicitEnumerationTypes);
            clrMissingEnumerationTypes.ExceptWith(clrExplicitEnumerationTypes);

            foreach (var clrMissingEnumerationType in clrMissingEnumerationTypes)
            {
                var apiMissingEnumerationTypeConfiguration = CreateApiEnumerationTypeConfiguration(clrMissingEnumerationType, apiPrecedenceStack);
                apiMutableSchema.ApiEnumerationTypeConfigurationDictionary.Add(clrMissingEnumerationType, apiMissingEnumerationTypeConfiguration);
            }
        }

        private static void AddMissingApiScalarTypes(ApiMutableSchema apiMutableSchema, ApiPrecedenceStack apiPrecedenceStack)
        {
            Contract.Requires(apiMutableSchema != null);
            Contract.Requires(apiPrecedenceStack != null);

            var clrExplicitScalarTypes = apiMutableSchema.ApiScalarTypeConfigurationDictionary
                                                         .Keys
                                                         .ToList();

            var clrMissingScalarTypes = new HashSet<Type>(apiMutableSchema.ClrImplicitScalarTypes);
            clrMissingScalarTypes.ExceptWith(clrExplicitScalarTypes);

            foreach (var clrMissingScalarType in clrMissingScalarTypes)
            {
                var apiMissingScalarTypeConfiguration = CreateApiScalarTypeConfiguration(clrMissingScalarType, apiPrecedenceStack);
                apiMutableSchema.ApiScalarTypeConfigurationDictionary.Add(clrMissingScalarType, apiMissingScalarTypeConfiguration);
            }
        }

        private void ApplyApiSchemaConventions(ApiSchemaFactorySettings apiSchemaFactorySettings)
        {
            var apiSchemaConventions = apiSchemaFactorySettings?.ApiConventionSet?.ApiSchemaConventions;
            if (apiSchemaConventions == null)
                return;

            var apiConventionSettings = apiSchemaFactorySettings.ApiConventionSettings;

            foreach (var apiSchemaConvention in apiSchemaConventions)
            {
                apiSchemaConvention.Apply(this, apiConventionSettings);
            }
        }

        private static ApiTypeConfiguration CreateApiEnumerationTypeConfiguration(Type clrEnumerationType, ApiPrecedenceStack apiPrecedenceStack)
        {
            Contract.Requires(clrEnumerationType != null);
            Contract.Requires(apiPrecedenceStack != null);

            var apiEnumerationTypeConfiguration = ApiTypeConfigurationFactory.CreateApiTypeConfiguration<ApiTypeConfiguration>(clrEnumerationType, ApiEnumerationTypeConfigurationOpenGenericType, apiPrecedenceStack);
            return apiEnumerationTypeConfiguration;
        }

        private static IEnumerable<IApiEnumerationType> CreateApiEnumerationTypes(ApiMutableSchema   apiMutableSchema,
                                                                                  ApiPrecedenceStack apiPrecedenceStack,
                                                                                  ApiSchemaProxy     apiSchemaProxy)
        {
            Contract.Requires(apiMutableSchema != null);
            Contract.Requires(apiPrecedenceStack != null);

            ApiFrameworkLog.Debug($"Creating {nameof(ApiEnumerationType)}s".Indent(IndentConstants.ApiEnumerationTypes));

            RemoveUnusedApiEnumerationTypes(apiMutableSchema);
            AddMissingApiEnumerationTypes(apiMutableSchema, apiPrecedenceStack);

            var apiEnumerationTypeConfigurations = apiMutableSchema.ApiEnumerationTypeConfigurationDictionary
                                                                   .Values
                                                                   .ToList();
            var clrExcludedEnumerationTypes = apiMutableSchema.ClrExcludedEnumerationTypes;
            var apiEnumerationTypes         = CreateApiTypes<IApiEnumerationType>(apiMutableSchema, apiSchemaProxy, apiEnumerationTypeConfigurations, clrExcludedEnumerationTypes);

            ApiFrameworkLog.Debug($"Created {nameof(ApiEnumerationType)}s".Indent(IndentConstants.ApiEnumerationTypes));

            return apiEnumerationTypes;
        }

        private static IEnumerable<TApiType> CreateApiTypes<TApiType>(ApiMutableSchema                          apiMutableSchema,
                                                                      ApiSchemaProxy                            apiSchemaProxy,
                                                                      IReadOnlyCollection<ApiTypeConfiguration> apiTypeConfigurations,
                                                                      ISet<Type>                                clrExcludedTypes)
            where TApiType : IApiNamedType
        {
            Contract.Requires(apiMutableSchema != null);
            Contract.Requires(apiSchemaProxy != null);
            Contract.Requires(apiTypeConfigurations != null);
            Contract.Requires(clrExcludedTypes != null);

            var apiTypeConfigurationsQuery = apiTypeConfigurations.AsEnumerable();
            if (clrExcludedTypes.Any())
            {
                var clrIncludedTypes = new HashSet<Type>(apiTypeConfigurations.Select(x => x.ClrType));
                clrIncludedTypes.ExceptWith(clrExcludedTypes);

                apiTypeConfigurationsQuery = apiTypeConfigurationsQuery.Where(x => clrIncludedTypes.Contains(x.ClrType));
            }

            var apiTypes = apiTypeConfigurationsQuery.Select(x =>
                                                     {
                                                         var apiMutableType = x.CreateApiMutableType(apiMutableSchema);
                                                         var tuple          = new Tuple<ApiTypeConfiguration, ApiMutableType>(x, apiMutableType);
                                                         return tuple;
                                                     })
                                                     .ToList()
                                                     .Select(tuple =>
                                                     {
                                                         var apiTypeConfiguration = tuple.Item1;
                                                         var apiMutableType       = tuple.Item2;
                                                         var apiType              = apiTypeConfiguration.CreateApiType(apiMutableType, apiSchemaProxy);
                                                         return apiType;
                                                     })
                                                     .Cast<TApiType>()
                                                     .OrderBy(x => x.ApiName)
                                                     .ToList();

            return apiTypes;
        }

        private Func<ApiSchemaFactorySettings, ApiMutableSchema> CreateApiMutableSchemaFactory()
        {
            ApiMutableSchema ApiMutableSchemaFactory(ApiSchemaFactorySettings apiSchemaFactorySettings)
            {
                // Apply conventions
                this.ApiPrecedenceStack.Push(ApiPrecedenceLevel.Convention);

                this.ApplyApiSchemaConventions(apiSchemaFactorySettings);

                this.ApiPrecedenceStack.Pop();

                // Create API schema context
                const string apiDefaultName = ApiSchema.ApiDefaultName;
                var apiMutableSchema = new ApiMutableSchema(apiSchemaFactorySettings)
                {
                    ApiName = apiDefaultName
                };
                return apiMutableSchema;
            }

            return ApiMutableSchemaFactory;
        }

        private static IApiSchema CreateApiSchema(ApiMutableSchema   apiMutableSchema,
                                                  ApiPrecedenceStack apiPrecedenceStack)
        {
            Contract.Requires(apiMutableSchema != null);
            Contract.Requires(apiPrecedenceStack != null);

            var apiSchemaProxy = new ApiSchemaProxy();

            // Create API object types before other API types so implicit primitive property types (enums, scalars, etc.) are added to the mutable schema object.
            var apiObjectTypes      = CreateApiObjectTypes(apiMutableSchema, apiSchemaProxy);
            var apiEnumerationTypes = CreateApiEnumerationTypes(apiMutableSchema, apiPrecedenceStack, apiSchemaProxy);
            var apiScalarTypes      = CreateApiScalarTypes(apiMutableSchema, apiPrecedenceStack, apiSchemaProxy);

            var apiSchemaName = apiMutableSchema.ApiName;
            var apiSchema     = ApiTypeFactory.CreateApiSchema(apiSchemaName, apiEnumerationTypes, apiObjectTypes, apiScalarTypes);

            apiSchemaProxy.Initialize(apiSchema);

            return apiSchema;
        }

        private static IEnumerable<IApiObjectType> CreateApiObjectTypes(ApiMutableSchema apiMutableSchema,
                                                                        ApiSchemaProxy   apiSchemaProxy)
        {
            Contract.Requires(apiMutableSchema != null);
            Contract.Requires(apiSchemaProxy != null);

            ApiFrameworkLog.Debug($"Creating {nameof(ApiObjectType)}s".Indent(IndentConstants.ApiObjectTypes));

            var apiObjectTypeConfigurations = apiMutableSchema.ApiObjectTypeConfigurationDictionary
                                                              .Values
                                                              .ToList();
            var clrExcludedObjectTypes = apiMutableSchema.ClrExcludedObjectTypes;
            var apiObjectTypes         = CreateApiTypes<IApiObjectType>(apiMutableSchema, apiSchemaProxy, apiObjectTypeConfigurations, clrExcludedObjectTypes);

            ApiFrameworkLog.Debug($"Created {nameof(ApiObjectType)}s".Indent(IndentConstants.ApiObjectTypes));

            return apiObjectTypes;
        }

        private static ApiTypeConfiguration CreateApiScalarTypeConfiguration(Type clrScalarType, ApiPrecedenceStack apiPrecedenceStack)
        {
            Contract.Requires(clrScalarType != null);
            Contract.Requires(apiPrecedenceStack != null);

            var apiScalarTypeConfiguration = ApiTypeConfigurationFactory.CreateApiTypeConfiguration<ApiTypeConfiguration>(clrScalarType, ApiScalarTypeConfigurationOpenGenericType, apiPrecedenceStack);
            return apiScalarTypeConfiguration;
        }

        private static IEnumerable<IApiScalarType> CreateApiScalarTypes(ApiMutableSchema   apiMutableSchema,
                                                                        ApiPrecedenceStack apiPrecedenceStack,
                                                                        ApiSchemaProxy     apiSchemaProxy)
        {
            Contract.Requires(apiMutableSchema != null);
            Contract.Requires(apiPrecedenceStack != null);
            Contract.Requires(apiSchemaProxy != null);

            ApiFrameworkLog.Debug($"Creating {nameof(ApiScalarType)}s".Indent(IndentConstants.ApiScalarTypes));

            RemoveUnusedApiScalarTypes(apiMutableSchema);
            AddMissingApiScalarTypes(apiMutableSchema, apiPrecedenceStack);

            var apiScalarTypeConfigurations = apiMutableSchema.ApiScalarTypeConfigurationDictionary
                                                              .Values
                                                              .ToList();
            var clrExcludedScalarTypes = apiMutableSchema.ClrExcludedScalarTypes;
            var apiScalarTypes         = CreateApiTypes<IApiScalarType>(apiMutableSchema, apiSchemaProxy, apiScalarTypeConfigurations, clrExcludedScalarTypes);

            ApiFrameworkLog.Debug($"Created {nameof(ApiScalarType)}s".Indent(IndentConstants.ApiScalarTypes));

            return apiScalarTypes;
        }

        private static void RemoveUnusedApiEnumerationTypes(ApiMutableSchema apiMutableSchema)
        {
            Contract.Requires(apiMutableSchema != null);

            var clrExplicitEnumerationTypes = apiMutableSchema.ApiEnumerationTypeConfigurationDictionary
                                                              .Keys
                                                              .ToList();

            var clrUnusedEnumerationTypes = new HashSet<Type>(clrExplicitEnumerationTypes);
            clrUnusedEnumerationTypes.ExceptWith(apiMutableSchema.ClrImplicitEnumerationTypes);

            foreach (var clrUnusedEnumerationType in clrUnusedEnumerationTypes)
            {
                apiMutableSchema.ApiEnumerationTypeConfigurationDictionary
                                .Remove(clrUnusedEnumerationType);
            }
        }

        private static void RemoveUnusedApiScalarTypes(ApiMutableSchema apiMutableSchema)
        {
            Contract.Requires(apiMutableSchema != null);

            var clrExplicitScalarTypes = apiMutableSchema.ApiScalarTypeConfigurationDictionary
                                                         .Keys
                                                         .ToList();

            var clrUnusedScalarTypes = new HashSet<Type>(clrExplicitScalarTypes);
            clrUnusedScalarTypes.ExceptWith(apiMutableSchema.ClrImplicitScalarTypes);

            foreach (var clrUnusedScalarType in clrUnusedScalarTypes)
            {
                apiMutableSchema.ApiScalarTypeConfigurationDictionary
                                .Remove(clrUnusedScalarType);
            }
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private static readonly Type ApiEnumerationTypeConfigurationOpenGenericType = typeof(ApiEnumerationTypeConfiguration<>);
        private static readonly Type ApiScalarTypeConfigurationOpenGenericType      = typeof(ApiScalarTypeConfiguration<>);

        private const string CallerTypeName = nameof(ApiSchemaConfiguration);
        #endregion
    }
}