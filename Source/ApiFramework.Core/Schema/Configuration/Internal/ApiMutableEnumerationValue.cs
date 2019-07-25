// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

namespace ApiFramework.Schema.Configuration.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiMutableEnumerationValue
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public string ApiName        { get; set; }
        public string ApiDescription { get; set; }
        public string ClrName        { get; set; }
        public int    ClrOrdinal     { get; set; }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            return $"{nameof(ApiMutableEnumerationValue)} [{nameof(this.ApiName)}={this.ApiName} {nameof(this.ClrName)}={this.ClrName} {nameof(this.ClrOrdinal)}={this.ClrOrdinal}]";
        }
        #endregion
    }
}
