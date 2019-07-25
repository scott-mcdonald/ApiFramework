// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace ApiFramework.Schema.Configuration.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiMutableScalarType : ApiMutableNamedType
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public Type ClrScalarType { get; set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            return $"{nameof(ApiMutableScalarType)} [{nameof(this.ApiName)}={this.ApiName} {nameof(this.ApiDescription)}='{this.ApiDescription}' {nameof(this.ClrScalarType)}={this.ClrScalarType.Name}]";
        }
        #endregion
    }
}
