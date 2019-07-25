// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace ApiFramework.Schema.Configuration.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiMutableCollectionType : ApiMutableType
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public ApiTypeKind      ApiItemTypeKind      { get; set; }
        public ApiTypeModifiers ApiItemTypeModifiers { get; set; }
        public Type             ClrDeclaringType     { get; set; }
        public Type             ClrCollectionType    { get; set; }
        public Type             ClrItemType          { get; set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            return $"{nameof(ApiMutableCollectionType)} [{nameof(this.ApiItemTypeModifiers)}={this.ApiItemTypeModifiers}]";
        }
        #endregion
    }
}
