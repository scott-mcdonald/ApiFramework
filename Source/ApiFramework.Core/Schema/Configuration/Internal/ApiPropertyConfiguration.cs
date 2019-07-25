// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq;

using ApiFramework.Schema.Conventions;
using ApiFramework.Schema.Internal;

namespace ApiFramework.Schema.Configuration.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiPropertyConfiguration : IApiPropertyBuilder
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiPropertyConfiguration(Type clrDeclaringType, string clrName, Type clrType, ApiPrecedenceStack apiPrecedenceStack)
        {
            Contract.Requires(clrDeclaringType != null);
            Contract.Requires(clrName.SafeHasContent());
            Contract.Requires(clrType != null);
            Contract.Requires(apiPrecedenceStack != null);

            this.ClrName                   = clrName;
            this.ClrType                   = clrType;
            this.ApiPrecedenceStack        = apiPrecedenceStack;
            this.ApiMutablePropertyFactory = new ApiMutableFactory<ApiMutableSchema, ApiMutableProperty>(this.CreateApiMutablePropertyFactory(clrDeclaringType, clrName, clrType), IndentConstants.ApiProperty);
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IApiPropertyBuilder Implementation
        public string ClrName { get; }
        public Type   ClrType { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiPropertyBuilder Implementation
        public IApiPropertyBuilder HasName(string apiName)
        {
            Contract.Requires(apiName.SafeHasContent());

            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutablePropertyFactory.AddModifier(apiConfigurationLevel,
                                                       x =>
                                                       {
                                                           ApiFrameworkLog.Trace($"Modified {nameof(x.ApiName)} '{x.ApiName}' => '{apiName}' at {apiConfigurationLevel} Level".Indent(IndentConstants.ApiMutablePropertyModifier));
                                                           x.ApiName = apiName;
                                                       },
                                                       CallerTypeName);
            return this;
        }

        public IApiPropertyBuilder HasDescription(string apiDescription)
        {
            Contract.Requires(apiDescription.SafeHasContent());

            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutablePropertyFactory.AddModifier(apiConfigurationLevel,
                                                       x =>
                                                       {
                                                           ApiFrameworkLog.Trace($"Modified {nameof(x.ApiDescription)} '{x.ApiDescription}' => '{apiDescription}' at {apiConfigurationLevel} Level".Indent(IndentConstants.ApiMutablePropertyModifier));
                                                           x.ApiDescription = apiDescription;
                                                       },
                                                       CallerTypeName);
            return this;
        }

        public IApiPropertyBuilder IsRequired()
        {
            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutablePropertyFactory.AddModifier(apiConfigurationLevel,
                                                       x =>
                                                       {
                                                           var apiTypeModifiers = x.ApiTypeModifiers;
                                                           x.ApiTypeModifiers |= ApiTypeModifiers.Required;
                                                           ApiFrameworkLog.Trace($"Modified {nameof(x.ApiTypeModifiers)} '{apiTypeModifiers}' => '{x.ApiTypeModifiers}' at {apiConfigurationLevel} Level".Indent(IndentConstants.ApiMutablePropertyModifier));
                                                       },
                                                       CallerTypeName);

            return this;
        }

        public IApiPropertyBuilder ApiCollectionType(Func<IApiCollectionTypeBuilder, IApiCollectionTypeBuilder> configuration)
        {
            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutablePropertyFactory.AddModifier(apiConfigurationLevel,
                                                       x =>
                                                       {
                                                           var apiCollectionTypeConfiguration = x.ApiCollectionTypeConfiguration;
                                                           configuration?.Invoke(apiCollectionTypeConfiguration);
                                                       },
                                                       CallerTypeName);
            return this;
        }
        #endregion

        #region Factory Methods
        public IApiProperty CreateApiProperty(ApiMutableSchema apiMutableSchema, ApiMutableObjectType apiMutableObjectType, ApiSchemaProxy apiSchemaProxy)
        {
            Contract.Requires(apiMutableSchema != null);
            Contract.Requires(apiMutableObjectType != null);
            Contract.Requires(apiSchemaProxy != null);

            var apiMutableProperty = this.ApiMutablePropertyFactory.Create(apiMutableSchema);

            var apiProperty = CreateApiProperty(apiMutableObjectType, apiMutableProperty, apiSchemaProxy);
            ApiFrameworkLog.Debug($"Created {apiProperty}".Indent(IndentConstants.ApiProperty));

            return apiProperty;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private ApiPrecedenceStack ApiPrecedenceStack { get; }

        private ApiMutableFactory<ApiMutableSchema, ApiMutableProperty> ApiMutablePropertyFactory { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void ApplyApiPropertyNameConventions(string clrName, IApiConventionSet apiConventionSet, ApiConventionSettings apiConventionSettings)
        {
            Contract.Requires(clrName.SafeHasContent());
            Contract.Requires(apiConventionSet != null);

            var apiPropertyNameConventions = apiConventionSet?.ApiPropertyNameConventions;
            if (apiPropertyNameConventions == null)
                return;

            var apiPropertyNameConventionsCollection = apiPropertyNameConventions.SafeToReadOnlyCollection();
            if (!apiPropertyNameConventionsCollection.Any())
                return;

            var apiName = clrName;
            apiName = apiPropertyNameConventionsCollection.Aggregate(apiName, (current, apiNamingConvention) => apiNamingConvention.Apply(current, apiConventionSettings));

            this.HasName(apiName);
        }

        private Func<ApiMutableSchema, ApiMutableProperty> CreateApiMutablePropertyFactory(Type clrDeclaringType, string clrName, Type clrType)
        {
            Contract.Requires(clrDeclaringType != null);
            Contract.Requires(clrName.SafeHasContent());
            Contract.Requires(clrType != null);

            ApiMutableProperty ApiMutablePropertyFactory(ApiMutableSchema apiMutableSchema)
            {
                Contract.Requires(apiMutableSchema != null);

                // Apply conventions
                this.ApiPrecedenceStack.Push(ApiPrecedenceLevel.Convention);

                var apiConventionSet      = apiMutableSchema?.ApiConventionSet;
                var apiConventionSettings = apiMutableSchema?.ApiConventionSettings;
                this.ApplyApiPropertyNameConventions(clrName, apiConventionSet, apiConventionSettings);

                this.ApiPrecedenceStack.Pop();

                // Create API mutable property
                var apiTypeKind = clrType.GetApiTypeKind(out var clrItemType);

                var apiCollectionTypeConfiguration = default(ApiCollectionTypeConfiguration);
                switch (apiTypeKind)
                {
                    case ApiTypeKind.Collection:
                    {
                        apiCollectionTypeConfiguration = new ApiCollectionTypeConfiguration(clrDeclaringType, clrType, clrItemType, this.ApiPrecedenceStack);
                        break;
                    }
                }

                var apiDefaultName = clrName;
                var apiMutableProperty = new ApiMutableProperty
                {
                    ApiMutableSchema               = apiMutableSchema,
                    ApiName                        = apiDefaultName,
                    ApiTypeKind                    = apiTypeKind,
                    ApiCollectionTypeConfiguration = apiCollectionTypeConfiguration,
                    ClrDeclaringType               = clrDeclaringType,
                    ClrName                        = clrName,
                    ClrType                        = clrType
                };
                return apiMutableProperty;
            }

            return ApiMutablePropertyFactory;
        }

        private static IApiProperty CreateApiProperty(ApiMutableObjectType apiMutableObjectType,
                                                      ApiMutableProperty apiMutableProperty,
                                                      ApiSchemaProxy apiSchemaProxy)
        {
            Contract.Requires(apiMutableObjectType != null);
            Contract.Requires(apiMutableProperty != null);
            Contract.Requires(apiSchemaProxy != null);

            var apiMutableSchema = apiMutableProperty.ApiMutableSchema;
            var apiName          = apiMutableProperty.ApiName;
            var apiDescription   = apiMutableProperty.ApiDescription;
            var apiTypeKind      = apiMutableProperty.ApiTypeKind;
            var apiTypeModifiers = apiMutableProperty.ApiTypeModifiers;
            var clrName          = apiMutableProperty.ClrName;
            var clrType          = apiMutableProperty.ClrType;

            switch (apiTypeKind)
            {
                case ApiTypeKind.Enumeration:
                {
                    apiMutableSchema.AddClrImplicitEnumerationType(clrType);
                    break;
                }

                case ApiTypeKind.Collection:
                {
                    // Create the known concrete API collection type.
                    var apiCollectionTypeConfiguration = apiMutableProperty.ApiCollectionTypeConfiguration;
                    var apiCollectionType              = apiCollectionTypeConfiguration.Create(apiMutableSchema, apiSchemaProxy);
                    return ApiTypeFactory.CreateApiProperty(apiName, apiDescription, apiCollectionType, apiTypeModifiers, clrName);
                }

                case ApiTypeKind.Scalar:
                {
                    apiMutableSchema.AddClrImplicitScalarType(clrType);
                    break;
                }
            }

            // API identity properties will always be required.
            var clrIdentityPropertyName = apiMutableObjectType?.ClrIdentityProperty?.ClrPropertyName;
            if (clrName == clrIdentityPropertyName)
            {
                apiTypeModifiers |= ApiTypeModifiers.Required;
            }

            var apiTypeResolver = new ApiSchemaProxyTypeResolver(apiSchemaProxy, apiTypeKind, clrType);
            return ApiTypeFactory.CreateApiProperty(apiName, apiDescription, apiTypeResolver, apiTypeModifiers, clrName);
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private const string CallerTypeName = nameof(ApiPropertyConfiguration);
        #endregion
    }
}