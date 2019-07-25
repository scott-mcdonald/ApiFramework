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
    internal class ApiRelationship : ExtensibleObject<IApiRelationship>, IApiRelationship
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiRelationship(IApiProperty               apiProperty,
                               ApiRelationshipCardinality apiCardinality,
                               IApiType                   apiRelatedType)
        {
            Contract.Requires(apiProperty != null);
            Contract.Requires(apiRelatedType != null);

            this.ApiProperty            = apiProperty;
            this.ApiCardinality         = apiCardinality;
            this.ApiRelatedTypeResolver = new ApiSimpleTypeResolver(apiRelatedType);
        }

        public ApiRelationship(IApiProperty               apiProperty,
                               ApiRelationshipCardinality apiCardinality,
                               IApiTypeResolver           apiRelatedTypeResolver)
        {
            Contract.Requires(apiProperty != null);
            Contract.Requires(apiRelatedTypeResolver != null);

            this.ApiProperty            = apiProperty;
            this.ApiCardinality         = apiCardinality;
            this.ApiRelatedTypeResolver = apiRelatedTypeResolver;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region IApiRelationship Implementation
        public IApiProperty ApiProperty { get; }

        public ApiRelationshipCardinality ApiCardinality { get; }

        public IApiObjectType ApiRelatedType => this.ApiRelatedTypeResolver.CanResolve() ? (IApiObjectType)this.ApiRelatedTypeResolver.Resolve() : null;
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            var apiRelatedTypeName = this.ApiRelatedType?.ApiName ?? "null";
            return $"{nameof(ApiRelationship)} [{nameof(this.ApiProperty.ApiName)}={this.ApiProperty.ApiName} {nameof(this.ApiCardinality)}={this.ApiCardinality} {nameof(this.ApiRelatedType)}={apiRelatedTypeName}]";
        }
        #endregion

        #region Visitor Methods
        public void Accept(IApiVisitor apiVisitor, int depth)
        {
            Contract.Requires(apiVisitor != null);

            apiVisitor.VisitApiRelationship(this, depth);
        }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region ExtensibleObject<T> Overrides
        protected override IApiRelationship ExtensionOwner => this;
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IApiTypeResolver ApiRelatedTypeResolver { get; }
        #endregion
    }
}