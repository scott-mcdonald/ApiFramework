// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Diagnostics.Contracts;

using ApiFramework.Extension;

namespace ApiFramework.Schema.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiProperty : ExtensibleObject<IApiProperty>, IApiProperty
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiProperty(string           apiName,
                           string           apiDescription,
                           IApiType         apiType,
                           ApiTypeModifiers apiTypeModifiers,
                           string           clrName)
        {
            Contract.Requires(apiName.SafeHasContent());
            Contract.Requires(apiType != null);
            Contract.Requires(clrName.SafeHasContent());

            this.ApiName          = apiName;
            this.ApiDescription   = apiDescription;
            this.ApiTypeResolver  = new ApiSimpleTypeResolver(apiType);
            this.ApiTypeModifiers = apiTypeModifiers;
            this.ClrName          = clrName;
        }

        public ApiProperty(string           apiName,
                           string           apiDescription,
                           IApiTypeResolver apiTypeResolver,
                           ApiTypeModifiers apiTypeModifiers,
                           string           clrName)
        {
            Contract.Requires(apiName.SafeHasContent());
            Contract.Requires(apiTypeResolver != null);
            Contract.Requires(clrName.SafeHasContent());

            this.ApiName          = apiName;
            this.ApiDescription   = apiDescription;
            this.ApiTypeResolver  = apiTypeResolver;
            this.ApiTypeModifiers = apiTypeModifiers;
            this.ClrName          = clrName;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IApiProperty Implementation
        public string ApiName { get; }

        public string ApiDescription { get; }

        public IApiType ApiType => this.ApiTypeResolver.CanResolve() ? this.ApiTypeResolver.Resolve() : null;

        public ApiTypeModifiers ApiTypeModifiers { get; }

        public string ClrName { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            var apiType = this.ApiType?.ToString() ?? "null";
            return $"{nameof(ApiProperty)} [{nameof(this.ApiName)}={this.ApiName} {nameof(this.ApiType)}={apiType} {nameof(this.ApiTypeModifiers)}={this.ApiTypeModifiers} {nameof(this.ClrName)}={this.ClrName}]";
        }
        #endregion

        #region Visitor Methods
        public void Accept(IApiVisitor apiVisitor, int depth)
        {
            Contract.Requires(apiVisitor != null);

            apiVisitor.VisitApiProperty(this, depth);
        }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region ExtensibleObject<T> Overrides
        protected override IApiProperty ExtensionOwner => this;
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IApiTypeResolver ApiTypeResolver { get; }
        #endregion
    }
}