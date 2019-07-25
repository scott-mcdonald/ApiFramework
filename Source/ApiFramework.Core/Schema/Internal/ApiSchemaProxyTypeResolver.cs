// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;
using System.Threading;

using ApiFramework.Exceptions;

namespace ApiFramework.Schema.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiSchemaProxyTypeResolver : IApiTypeResolver
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiSchemaProxyTypeResolver(IApiSchemaProxy apiSchemaProxy,
                                          ApiTypeKind     apiTypeKind,
                                          Type            clrType)
        {
            Contract.Requires(apiSchemaProxy != null);
            Contract.Requires(clrType != null);

            var clrActualType = ClrTypeUtilities.GetActualType(clrType);

            this.ApiSchemaProxy = apiSchemaProxy;
            this.ApiTypeKind    = apiTypeKind;
            this.ClrType        = clrActualType;

            this.LazyResolvedApiType = new Lazy<IApiType>(this.ResolveImpl, LazyThreadSafetyMode.PublicationOnly);
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region IApiTypeResolver Implementation
        public bool CanResolve()
        {
            return this.ApiSchemaProxy.Subject != null;
        }

        public IApiType Resolve()
        {
            return this.LazyResolvedApiType.Value;
        }
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IApiSchemaProxy ApiSchemaProxy { get; }

        private ApiTypeKind ApiTypeKind { get; }

        private Type ClrType { get; }

        private Lazy<IApiType> LazyResolvedApiType { get; }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region MyRegion
        private IApiType ResolveImpl()
        {
            var apiSchema   = this.ApiSchemaProxy.Subject;
            var apiTypeKind = this.ApiTypeKind;
            var clrType     = this.ClrType;
            switch (apiTypeKind)
            {
                case ApiTypeKind.Enumeration: return apiSchema.GetApiEnumerationType(clrType);
                case ApiTypeKind.Object:      return apiSchema.GetApiObjectType(clrType);
                case ApiTypeKind.Scalar:      return apiSchema.GetApiScalarType(clrType);

                default:
                {
                    var message = $"Unable to resolve API type [apiTypeKind={apiTypeKind} clrType={clrType.Name}]";
                    throw new ApiSchemaException(message);
                }
            }
        }
        #endregion
    }
}