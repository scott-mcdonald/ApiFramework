// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;

namespace ApiFramework.Schema.Configuration.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiMutableProperty
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public ApiMutableSchema               ApiMutableSchema               { get; set; }
        public string                         ApiName                        { get; set; }
        public string                         ApiDescription                 { get; set; }
        public ApiTypeKind                    ApiTypeKind                    { get; set; }
        public ApiTypeModifiers               ApiTypeModifiers               { get; set; }
        public ApiCollectionTypeConfiguration ApiCollectionTypeConfiguration { get; set; }
        public Type                           ClrDeclaringType               { get; set; }
        public string                         ClrName                        { get; set; }
        public Type                           ClrType                        { get; set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            return $"{nameof(ApiMutableProperty)} [{nameof(this.ApiName)}={this.ApiName} {nameof(this.ApiTypeModifiers)}={this.ApiTypeModifiers} {nameof(this.ClrName)}={this.ClrName}]";
        }
        #endregion
    }
}