// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

using ApiFramework.Exceptions;
using ApiFramework.Schema.Internal;

namespace ApiFramework.Schema.Configuration.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiCollectionTypeConfiguration : ApiTypeConfiguration, IApiCollectionTypeBuilder
    {
        // INTERNAL CONSTRUCTORS ////////////////////////////////////////////
        #region Constructors
        public ApiCollectionTypeConfiguration(Type clrDeclaringType, Type clrType, Type clrItemType, ApiPrecedenceStack apiPrecedenceStack)
            : base(clrType)
        {
            Contract.Requires(clrDeclaringType != null);
            Contract.Requires(clrType != null);
            Contract.Requires(clrItemType != null);
            Contract.Requires(apiPrecedenceStack != null);

            this.ApiPrecedenceStack              = apiPrecedenceStack;
            this.ApiMutableCollectionTypeFactory = new ApiMutableFactory<ApiMutableSchema, ApiMutableCollectionType>(this.CreateApiMutableCollectionTypeFactory(clrDeclaringType, clrType, clrItemType), IndentConstants.ApiMutableCollectionType);
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiCollectionTypeBuilder Implementation
        public IApiCollectionTypeBuilder ItemIsRequired()
        {
            var apiConfigurationLevel = this.ApiPrecedenceStack.CurrentLevel;
            this.ApiMutableCollectionTypeFactory.AddModifier(apiConfigurationLevel,
                                                             x =>
                                                             {
                                                                 var apiItemTypeModifiers = x.ApiItemTypeModifiers;
                                                                 x.ApiItemTypeModifiers |= ApiTypeModifiers.Required;
                                                                 ApiFrameworkLog.Trace(
                                                                     $"Modified {nameof(x.ApiItemTypeModifiers)} '{apiItemTypeModifiers}' => '{x.ApiItemTypeModifiers}' at {apiConfigurationLevel} Level".Indent(
                                                                         IndentConstants.ApiMutableCollectionTypeModifier));
                                                             },
                                                             CallerTypeName);
            return this;
        }
        #endregion

        #region Factory Methods
        public IApiCollectionType Create(ApiMutableSchema apiMutableSchema, ApiSchemaProxy apiSchemaProxy)
        {
            Contract.Requires(apiMutableSchema != null);
            Contract.Requires(apiSchemaProxy != null);

            var apiMutableCollectionType = this.CreateApiMutableType(apiMutableSchema);

            var apiCollectionType = (IApiCollectionType)this.CreateApiType(apiMutableCollectionType, apiSchemaProxy);
            ApiFrameworkLog.Debug($"Created {apiCollectionType}".Indent(IndentConstants.ApiCollectionType));

            return apiCollectionType;
        }
        #endregion

        // INTERNAL PROPERTIES //////////////////////////////////////////////
        #region ApiTypeConfiguration Overrides
        internal override ApiTypeKind ApiTypeKind => ApiTypeKind.Collection;

        internal override ApiMutableFactory ApiMutableFactory => this.ApiMutableCollectionTypeFactory;
        #endregion

        // INTERNAL METHODS /////////////////////////////////////////////////
        #region ApiTypeConfiguration Overrides
        internal override ApiMutableType CreateApiMutableType(ApiMutableSchema apiMutableSchema)
        {
            Contract.Requires(apiMutableSchema != null);

            var apiMutableCollectionType = this.ApiMutableCollectionTypeFactory.Create(apiMutableSchema);
            return apiMutableCollectionType;
        }

        internal override IApiType CreateApiType(ApiMutableType apiMutableType, ApiSchemaProxy apiSchemaProxy)
        {
            Contract.Requires(apiMutableType != null);
            Contract.Requires(apiSchemaProxy != null);

            var apiMutableCollectionType = (ApiMutableCollectionType)apiMutableType;

            var apiCollectionType = CreateApiCollectionType(apiMutableCollectionType, apiSchemaProxy);
            return apiCollectionType;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private ApiPrecedenceStack ApiPrecedenceStack { get; }

        private ApiMutableFactory<ApiMutableSchema, ApiMutableCollectionType> ApiMutableCollectionTypeFactory { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private Func<ApiMutableSchema, ApiMutableCollectionType> CreateApiMutableCollectionTypeFactory(Type clrDeclaringType, Type clrType, Type clrItemType)
        {
            Contract.Requires(clrDeclaringType != null);
            Contract.Requires(clrType != null);
            Contract.Requires(clrItemType != null);

            ApiMutableCollectionType ApiMutableCollectionTypeFactory(ApiMutableSchema apiMutableSchema)
            {
                Contract.Requires(apiMutableSchema != null);

                var apiItemTypeKind = clrItemType.GetApiTypeKind(out var clrInnerCollectionItemType);
                switch (apiItemTypeKind)
                {
                    case ApiTypeKind.Collection:
                        // Unable to handle collections within collections.
                        var message = $"Unable to configure collections [clrItemType={clrItemType.Name}] that contain other collections [clrItemType={clrInnerCollectionItemType.Name}] in the API schema.";
                        throw new ApiSchemaException(message);
                }

                var apiMutableCollectionType = new ApiMutableCollectionType
                {
                    ApiMutableSchema  = apiMutableSchema,
                    ApiItemTypeKind   = apiItemTypeKind,
                    ClrDeclaringType  = clrDeclaringType,
                    ClrCollectionType = clrType,
                    ClrItemType       = clrItemType,
                };
                return apiMutableCollectionType;
            }

            return ApiMutableCollectionTypeFactory;
        }

        private static IApiCollectionType CreateApiCollectionType(ApiMutableCollectionType apiMutableCollectionType, ApiSchemaProxy apiSchemaProxy)
        {
            Contract.Requires(apiMutableCollectionType != null);
            Contract.Requires(apiSchemaProxy != null);

            var apiMutableSchema     = apiMutableCollectionType.ApiMutableSchema;
            var apiItemTypeKind      = apiMutableCollectionType.ApiItemTypeKind;
            var apiItemTypeModifiers = apiMutableCollectionType.ApiItemTypeModifiers;
            var clrItemType          = apiMutableCollectionType.ClrItemType;

            switch (apiItemTypeKind)
            {
                case ApiTypeKind.Scalar:
                {
                    apiMutableSchema.AddClrImplicitScalarType(clrItemType);
                    break;
                }

                case ApiTypeKind.Enumeration:
                {
                    apiMutableSchema.AddClrImplicitEnumerationType(clrItemType);
                    break;
                }
            }

            var apiItemTypeResolver = new ApiSchemaProxyTypeResolver(apiSchemaProxy, apiItemTypeKind, clrItemType);
            var apiCollectionType   = ApiTypeFactory.CreateApiCollectionType(apiItemTypeResolver, apiItemTypeModifiers);

            return apiCollectionType;
        }
        #endregion

        // PRIVATE FIELDS ///////////////////////////////////////////////////
        #region Constants
        private const string CallerTypeName = nameof(ApiCollectionTypeConfiguration);
        #endregion
    }
}