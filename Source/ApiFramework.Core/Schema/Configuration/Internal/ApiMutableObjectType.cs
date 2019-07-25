// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;

namespace ApiFramework.Schema.Configuration.Internal
{
    /// <summary>
    /// This API supports the API Framework infrastructure and is not intended to be used directly from your code.
    /// This API may change or be removed in future releases.
    /// </summary>
    internal class ApiMutableObjectType : ApiMutableNamedType
    {
        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        public IList<ApiPropertyConfiguration> ApiPropertyConfigurations              { get; } = new List<ApiPropertyConfiguration>();
        public IDictionary<string, int>        ApiPropertyConfigurationsIndex         { get; } = new Dictionary<string, int>();
        public Type                            ClrObjectType                          { get; set; }
        public ClrIdentityProperty             ClrIdentityProperty                    { get; set; }
        public IList<ClrRelationshipProperty>  ClrRelationshipPropertyCollection      { get; } = new List<ClrRelationshipProperty>();
        public IDictionary<string, int>        ClrRelationshipPropertyIndexDictionary { get; } = new Dictionary<string, int>();
        public ISet<string>                    ClrExcludedPropertyNames               { get; } = new HashSet<string>();
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Object Overrides
        public override string ToString()
        {
            return $"{nameof(ApiMutableObjectType)} [{nameof(this.ApiName)}={this.ApiName} {nameof(this.ApiDescription)}='{this.ApiDescription}' {nameof(this.ClrObjectType)}={this.ClrObjectType.Name}]";
        }
        #endregion
    }
}