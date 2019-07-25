// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Linq;

using ApiFramework.Schema.Configuration.Internal;
using ApiFramework.Schema.Conventions;
using ApiFramework.Schema.Internal;

namespace ApiFramework.Schema.Configuration
{
    /// <inheritdoc cref="IApiScalarTypeBuilder"/>
    /// <summary>
    /// Represents the configuration to build and create an API scalar type in an API schema.
    /// </summary>
    public abstract class ApiScalarTypeConfiguration : ApiTypeConfiguration, IApiScalarTypeBuilder
    {
        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiScalarTypeBuilder Implementation
        IApiScalarTypeBuilder IApiScalarTypeBuilder.HasName(string apiName)
        {
            Contract.Requires(apiName.SafeHasContent());

            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutableScalarTypeFactory.AddModifier(apiConfigurationLevel,
                                                         x =>
                                                         {
                                                             ApiFrameworkLog.Trace($"Modified {nameof(x.ApiName)} '{x.ApiName}' => '{apiName}' at {apiConfigurationLevel} Level".Indent(IndentConstants.ApiMutableScalarTypeModifier));
                                                             x.ApiName = apiName;
                                                         },
                                                         CallerTypeName);
            return this;
        }

        IApiScalarTypeBuilder IApiScalarTypeBuilder.HasDescription(string apiDescription)
        {
            Contract.Requires(apiDescription.SafeHasContent());

            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutableScalarTypeFactory.AddModifier(apiConfigurationLevel,
                                                         x =>
                                                         {
                                                             ApiFrameworkLog.Trace($"Modified {nameof(x.ApiDescription)} '{x.ApiDescription}' => '{apiDescription}' at {apiConfigurationLevel} Level".Indent(IndentConstants.ApiMutableScalarTypeModifier));
                                                             x.ApiDescription = apiDescription;
                                                         },
                                                         CallerTypeName);
            return this;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ApiScalarTypeConfiguration(Type clrScalarType, ApiPrecedenceStack apiPrecedenceStack)
            : base(clrScalarType)
        {
            Contract.Requires(clrScalarType != null);
            Contract.Requires(apiPrecedenceStack != null);

            this.ApiPrecedenceStack          = apiPrecedenceStack;
            this.ApiMutableScalarTypeFactory = new ApiMutableFactory<ApiMutableSchema, ApiMutableScalarType>(this.CreateApiMutableScalarTypeFactory(clrScalarType), IndentConstants.ApiMutableScalarType);
        }

        internal ApiScalarTypeConfiguration(Type clrScalarType, ApiPrecedenceStack apiPrecedenceStack, ApiMutableFactory<ApiMutableSchema, ApiMutableScalarType> apiMutableScalarTypeFactory)
            : base(clrScalarType)
        {
            Contract.Requires(clrScalarType != null);
            Contract.Requires(apiPrecedenceStack != null);

            this.ApiPrecedenceStack          = apiPrecedenceStack;
            this.ApiMutableScalarTypeFactory = apiMutableScalarTypeFactory;
        }
        #endregion

        // INTERNAL PROPERTIES //////////////////////////////////////////////
        #region ApiTypeConfiguration Overrides
        internal override ApiTypeKind ApiTypeKind => ApiTypeKind.Scalar;

        internal override ApiMutableFactory ApiMutableFactory => this.ApiMutableScalarTypeFactory;
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region ApiTypeConfiguration Overrides
        internal override ApiMutableType CreateApiMutableType(ApiMutableSchema apiMutableSchema)
        {
            Contract.Requires(apiMutableSchema != null);

            var apiMutableScalarType = this.ApiMutableScalarTypeFactory.Create(apiMutableSchema);
            return apiMutableScalarType;
        }

        internal override IApiType CreateApiType(ApiMutableType apiMutableType, ApiSchemaProxy apiSchemaProxy)
        {
            Contract.Requires(apiMutableType != null);
            Contract.Requires(apiSchemaProxy != null);

            var apiMutableScalarType = (ApiMutableScalarType)apiMutableType;

            var apiScalarType = CreateApiScalarType(apiMutableScalarType);
            ApiFrameworkLog.Debug($"Created {apiScalarType}".Indent(IndentConstants.ApiScalarType));

            return apiScalarType;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private ApiPrecedenceStack ApiPrecedenceStack { get; }

        private ApiMutableFactory<ApiMutableSchema, ApiMutableScalarType> ApiMutableScalarTypeFactory { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void ApplyApiScalarTypeNameConventions(Type clrScalarType, IApiConventionSet apiConventionSet, ApiConventionSettings apiConventionSettings)
        {
            Contract.Requires(clrScalarType != null);
            Contract.Requires(apiConventionSet != null);

            var apiScalarTypeNameConventions = apiConventionSet?.ApiScalarTypeNameConventions;
            if (apiScalarTypeNameConventions == null)
                return;

            var apiScalarTypeNameConventionsCollection = apiScalarTypeNameConventions.SafeToReadOnlyCollection();
            if (!apiScalarTypeNameConventionsCollection.Any())
                return;

            var apiName = clrScalarType.Name;
            apiName = apiScalarTypeNameConventionsCollection.Aggregate(apiName, (current, apiNamingConvention) => apiNamingConvention.Apply(current, apiConventionSettings));

            var apiScalarTypeBuilder = (IApiScalarTypeBuilder)this;
            apiScalarTypeBuilder.HasName(apiName);
        }

        private Func<ApiMutableSchema, ApiMutableScalarType> CreateApiMutableScalarTypeFactory(Type clrScalarType)
        {
            Contract.Requires(clrScalarType != null);

            ApiMutableScalarType ApiMutableScalarTypeFactory(ApiMutableSchema apiMutableSchema)
            {
                Contract.Requires(apiMutableSchema != null);

                // Apply conventions
                this.ApiPrecedenceStack.Push(ApiPrecedenceLevel.Convention);

                var apiConventionSet      = apiMutableSchema?.ApiConventionSet;
                var apiConventionSettings = apiMutableSchema?.ApiConventionSettings;
                this.ApplyApiScalarTypeNameConventions(clrScalarType, apiConventionSet, apiConventionSettings);

                this.ApiPrecedenceStack.Pop();

                // Create API scalar type context
                var apiDefaultName        = clrScalarType.Name;
                var apiDefaultDescription = clrScalarType.CreateDefaultApiScalarTypeDescription();
                var apiMutableScalarType = new ApiMutableScalarType
                {
                    ApiMutableSchema = apiMutableSchema,
                    ApiName          = apiDefaultName,
                    ApiDescription   = apiDefaultDescription,
                    ClrScalarType    = clrScalarType
                };
                return apiMutableScalarType;
            }

            return ApiMutableScalarTypeFactory;
        }

        private static IApiScalarType CreateApiScalarType(ApiMutableScalarType apiMutableScalarType)
        {
            Contract.Requires(apiMutableScalarType != null);

            var apiName        = apiMutableScalarType.ApiName;
            var apiDescription = apiMutableScalarType.ApiDescription;
            var clrScalarType  = apiMutableScalarType.ClrScalarType;
            var apiScalarType  = ApiTypeFactory.CreateApiScalarType(apiName, apiDescription, clrScalarType);
            return apiScalarType;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private const string CallerTypeName = nameof(ApiScalarTypeConfiguration);
        #endregion
    }

    /// <inheritdoc cref="IApiScalarTypeBuilder{TScalar}"/>
    /// <inheritdoc cref="ApiScalarTypeConfiguration"/>
    /// <typeparam name="TScalar">
    /// The CLR scalar type associated to the API scalar type for CLR serializing/deserializing purposes.
    /// </typeparam>
    public class ApiScalarTypeConfiguration<TScalar> : ApiScalarTypeConfiguration, IApiScalarTypeBuilder<TScalar>
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiScalarTypeConfiguration()
            : base(typeof(TScalar), new ApiPrecedenceStack(ApiPrecedenceLevel.TypeConfiguration))
        {
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiScalarTypeBuilder<TScalar> Implementation
        public IApiScalarTypeBuilder<TScalar> HasName(string apiName)
        {
            Contract.Requires(apiName.SafeHasContent());

            var apiScalarTypeBuilder = (IApiScalarTypeBuilder)this;
            apiScalarTypeBuilder.HasName(apiName);
            return this;
        }

        public IApiScalarTypeBuilder<TScalar> HasDescription(string apiDescription)
        {
            Contract.Requires(apiDescription.SafeHasContent());

            var apiScalarTypeBuilder = (IApiScalarTypeBuilder)this;
            apiScalarTypeBuilder.HasDescription(apiDescription);
            return this;
        }
        #endregion

        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        internal ApiScalarTypeConfiguration(ApiPrecedenceStack apiPrecedenceStack)
            : base(typeof(TScalar), apiPrecedenceStack)
        {
        }

        internal ApiScalarTypeConfiguration(ApiPrecedenceStack apiPrecedenceStack, ApiMutableFactory<ApiMutableSchema, ApiMutableScalarType> apiMutableScalarTypeFactory)
            : base(typeof(TScalar), apiPrecedenceStack, apiMutableScalarTypeFactory)
        {
        }
        #endregion
    }
}