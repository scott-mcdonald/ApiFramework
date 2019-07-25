// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Diagnostics.Contracts;

namespace ApiFramework.Schema.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiScalarType : ApiNamedType<IApiScalarType>, IApiScalarType
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public ApiScalarType(string apiName,
                             string apiDescription,
                             Type   clrScalarType)
            : base(apiName, apiDescription, clrScalarType)
        {
            Contract.Requires(clrScalarType != null);
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region ApiType Overrides
        public override ApiTypeKind ApiTypeKind => ApiTypeKind.Scalar;
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            return $"{nameof(ApiScalarType)} [{nameof(this.ApiName)}={this.ApiName} {nameof(this.ClrType)}={this.ClrType.Name}]";
        }
        #endregion

        // PROTECTED PROPERTIES /////////////////////////////////////////////
        #region ExtensibleObject<T> Overrides
        protected override IApiScalarType ExtensionOwner => this;
        #endregion
    }
}