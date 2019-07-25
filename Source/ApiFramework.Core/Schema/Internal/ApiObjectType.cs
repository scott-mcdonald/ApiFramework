// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ApiFramework.Schema.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiObjectType : ApiNamedType<IApiObjectType>, IApiObjectType
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiObjectType(string                        apiName,
                             string                        apiDescription,
                             IEnumerable<IApiProperty>     apiProperties,
                             IApiIdentity                  apiIdentity,
                             IEnumerable<IApiRelationship> apiRelationships,
                             Type                          clrObjectType)
            : base(apiName, apiDescription, clrObjectType)
        {
            Contract.Requires(apiName.SafeHasContent());
            Contract.Requires(clrObjectType != null);

            this.ApiProperties    = apiProperties.SafeToReadOnlyCollection();
            this.ApiIdentity      = apiIdentity;
            this.ApiRelationships = apiRelationships.SafeToReadOnlyCollection();

            this.ApiPropertyByApiNameDictionary = this.ApiProperties.ToDictionary(x => x.ApiName);
            this.ApiPropertyByClrNameDictionary = this.ApiProperties.ToDictionary(x => x.ClrName);

            this.ApiRelationshipByApiNameDictionary = this.ApiRelationships.ToDictionary(x => x.ApiProperty.ApiName);
            this.ApiRelationshipByClrNameDictionary = this.ApiRelationships.ToDictionary(x => x.ApiProperty.ClrName);
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region ApiType Overrides
        public override ApiTypeKind ApiTypeKind => ApiTypeKind.Object;
        #endregion

        #region IApiObjectType Implementation
        public IEnumerable<IApiProperty>     ApiProperties    { get; }
        public IApiIdentity                  ApiIdentity      { get; }
        public IEnumerable<IApiRelationship> ApiRelationships { get; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            return $"{nameof(ApiObjectType)} [{nameof(this.ApiName)}={this.ApiName} {nameof(ApiObjectTypeKind)}={this.GetApiObjectTypeKind()} {nameof(this.ClrType)}={this.ClrType.Name}]";
        }
        #endregion

        #region IApiObjectType Implementation
        public bool TryGetApiPropertyByApiName(string apiName, out IApiProperty apiProperty)
        {
            Contract.Requires(apiName != null);

            return this.ApiPropertyByApiNameDictionary.TryGetValue(apiName, out apiProperty);
        }

        public bool TryGetApiPropertyByClrName(string clrName, out IApiProperty apiProperty)
        {
            Contract.Requires(clrName != null);

            return this.ApiPropertyByClrNameDictionary.TryGetValue(clrName, out apiProperty);
        }

        public bool TryGetApiRelationshipByApiName(string apiName, out IApiRelationship apiRelationship)
        {
            Contract.Requires(apiName != null);

            return this.ApiRelationshipByApiNameDictionary.TryGetValue(apiName, out apiRelationship);
        }

        public bool TryGetApiRelationshipByClrName(string clrName, out IApiRelationship apiRelationship)
        {
            Contract.Requires(clrName != null);

            return this.ApiRelationshipByClrNameDictionary.TryGetValue(clrName, out apiRelationship);
        }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region ExtensibleObject<T> Overrides
        protected override IApiObjectType ExtensionOwner => this;
        #endregion

        // PRIVATE PROPERTIES ///////////////////////////////////////////////
        #region Properties
        private IDictionary<string, IApiProperty> ApiPropertyByApiNameDictionary { get; }
        private IDictionary<string, IApiProperty> ApiPropertyByClrNameDictionary { get; }

        private IDictionary<string, IApiRelationship> ApiRelationshipByApiNameDictionary { get; }
        private IDictionary<string, IApiRelationship> ApiRelationshipByClrNameDictionary { get; }
        #endregion
    }
}