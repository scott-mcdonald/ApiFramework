// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

namespace ApiFramework.Schema.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiCollectionType : ApiType<IApiCollectionType>, IApiCollectionType
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiCollectionType(IApiType         apiItemType,
                                 ApiTypeModifiers apiItemTypeModifiers)
        {
            Contract.Requires(apiItemType != null);

            this.ApiItemTypeResolver  = new ApiSimpleTypeResolver(apiItemType);
            this.ApiItemTypeModifiers = apiItemTypeModifiers;
        }

        public ApiCollectionType(IApiTypeResolver apiItemTypeResolver,
                                 ApiTypeModifiers apiItemTypeModifiers)
        {
            Contract.Requires(apiItemTypeResolver != null);

            this.ApiItemTypeResolver  = apiItemTypeResolver;
            this.ApiItemTypeModifiers = apiItemTypeModifiers;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region ApiType Overrides
        public override ApiTypeKind ApiTypeKind => ApiTypeKind.Collection;
        #endregion

        #region IApiCollectionType Implementation
        public IApiType ApiItemType => this.ApiItemTypeResolver.CanResolve() ? this.ApiItemTypeResolver.Resolve() : null;

        public ApiTypeModifiers ApiItemTypeModifiers { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            var apiItemType = this.ApiItemType?.ToString() ?? "null";
            return $"{nameof(ApiCollectionType)} [{nameof(this.ApiItemType)}={apiItemType} {nameof(this.ApiItemTypeModifiers)}={this.ApiItemTypeModifiers}]";
        }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region ExtensibleObject<T> Overrides
        protected override IApiCollectionType ExtensionOwner => this;
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IApiTypeResolver ApiItemTypeResolver { get; }
        #endregion
    }
}